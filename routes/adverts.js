const express = require('express');
const mongoose = require('mongoose');
const checkAuth = require('../middleware/check-auth');

const router = express.Router();
const Advert = require('../models/advert');
const Cabin = require('../models/cabin');
const { request } = require('../app');
const user = require('../models/user');
const advert = require('../models/advert');

// om det kommer en GET request på products
router.get('/', (req, res, next) => {
    Advert.find().exec()
        .then(documents => {
            res.status(200).json(documents); 
        })
        .catch(error => {
            console.log(error);
            const err = new Error(error);
            err.status = error.status || 500;
            
            next(err)
        });
});

// posta in en annons på vald stuga
router.post('/', checkAuth, (req, res, next) => {
    // eventlistener för att hitta stugan enligt dess id som man angivit
    Cabin.findById(req.body.cabin).exec()
    .then(cabinId => {
    const advert = new Advert({
        _id:    new mongoose.Types.ObjectId(),
        cabin:  cabinId,
        from:   req.body.from,
        to:     req.body.to,
        owner:  req.user.userId,
    });
    advert.save()
        .then(result => {
            console.log(result);
            res.status(201).json({
                message: 'Advert successfully created',
                advert: advert
            });
        })
        .catch(error => {
            console.log(error);
            const err = new Error(error);
            err.status = error.status || 500;
            
            next(err)
        });
    });
});

router.patch('/:id', checkAuth, (req, res, next) => {
    // spara annons-id:n för senare jämförelse
    advertCompare = req.url.replace('/','');
    
    Advert.findById(advertCompare, function(err, document) {
        if(err) res.status(404);

        // om en annan inloggad användare försöker modifiera en annons som inte tillhör till hen
        if(document.owner != req.user.userId) {
            res.status(403).json({
                message: 'permission denied'
            })
        } else {
            Cabin.findById(req.body.cabin).exec()
            .then(cabinId => {
            // de objekt som vi vill uppdatera i adverts
            const objForUpdate = {};
            // ifall bodyn innehåller ny information av det vi vill uppdatera så skicka in i vår objForUpdate
            // på detta sätt får vi inte "null" in i vår databas om vi har tomma strängar i vår post request
            if(req.body.cabin && !null) objForUpdate.cabin = cabinId;
            if(req.body.from)  objForUpdate.from =  req.body.from;
            if(req.body.to)    objForUpdate.to =    req.body.to;
            Advert.updateOne({ _id: req.params.id}, objForUpdate).exec()
            .then(result => {
                res.status(200).json({
                    message: "advert successfully updated"
                })
            })
            .catch(error => {
                console.log(error);
                    const err = new Error(error);
                    err.status = error.status || 500;
                    
                    next(err)
            });
            })
            .catch(error => {
                console.log(error);
                    const err = new Error(error);
                    err.status = error.status || 500;
                    
                    next(err)
            });
        }
    });
});

module.exports = router;