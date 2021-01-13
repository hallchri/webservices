// hur vi skall hantera ett meddelande som kommit in till "products"

const express = require('express');
const mongoose = require('mongoose');
const checkAuth = require('../middleware/check-auth');
const jwt = require('jsonwebtoken');

const router = express.Router();
const Cabin = require('../models/cabin');
const User = require('../models/user');

// om det kommer en GET request på products
router.get('/', (req, res, next) => {
    Cabin.find().exec()
        .then(documents => {
            res.status(200).json(documents); // documents kommer att innehålla ett json-array av våra cabin objekt
        })
        .catch(error => {
            console.log(error);
            const err = new Error(error);
            err.status = error.status || 500;
            
            next(err) // bollar vidare error-meddelandet till nästa middleware i felhanteringen
        });
});

// få GET respons på specifik ID, har vi kolon så hanterar det sökningen som en variabel
router.get('/:id', (req, res, next) => {

    const id = req.params.id; // för att komma åt specifik ID

    Cabin.findById(id).exec()
        .then(document => {
            res.status(200).json(document); // documents kommer att innehålla ett json-array av våra cabin objekt
        })
        .catch(error => {
            console.log(error);
            const err = new Error(error);
            err.status = error.status || 500;
            
            next(err) // bollar vidare error-meddelandet till nästa middleware i felhanteringen
        });
});

// eventlistener om det kommer en POST request på products
// går hand i hand med definitionen i cabin.js
router.post('/', checkAuth, (req, res, next) => {
    var firstname = "";
    var lastname = "";
    // sätt in automatiskt annonsskaparens namn
    User.findById(req.user.userId, function(err, document) {
        if(err) res.status(404);
        
        firstname += document.firstname;
        lastname += document.lastname;

    const cabin = new Cabin({
        _id:    new mongoose.Types.ObjectId(),
        owner:  firstname + " " + lastname,
        adress: req.body.adress,
        m2:     req.body.m2,
        sauna:  req.body.sauna,
        beach:  req.body.beach
    });

    // spara mitt cabin-objekt i mongodb databasen
    cabin.save()
        .then(result => {
            console.log(result);
            res.status(201).json({
                message: 'Cabin successfully created',
                cabin: cabin
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

router.put('/', (req, res, next) => {
    res.status(200).json({ 
        message: 'PUT request received on products'
    });
});

router.delete('/:id', (req, res, next) => {
    Cabin.remove({_id: req.params.id}).exec()
        .then(result => {
            res.status(200).json({
                message: 'Cabin deleted successfully'
            });
        })
        .catch();
});

//kom ihåg att göra våra nödvändiga exporteringar, nu är variabeln router exporterbar 
module.exports = router;