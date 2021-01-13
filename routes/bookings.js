const express = require('express');
const mongoose = require('mongoose');
const checkAuth = require('../middleware/check-auth');
const router = express.Router();
const Booking = require('../models/booking');
const Advert = require('../models/advert');
const Cabin = require('../models/cabin');
const { request } = require('../app');

// om det kommer en GET request på products
router.get('/', (req, res, next) => {
    Booking.find()
        .populate('cabin', 'cabin_id')
        .exec()
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

    Booking.findById(id).exec()
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

router.post('/', checkAuth, (req, res, next) => {
    // söker efter den specifika annonsen
    Advert.findById(req.body.advert).exec()
    .then(advertId => {
    const from =    new Date(req.body.from); // new Date() av våra datum så de går lätt att jämföra
    const to =      new Date(req.body.to);
    var booking =   new Booking({
        _id: new mongoose.Types.ObjectId(),
        advert: advertId,
        from: from,
        to: to
    });
    
    // kontrollera om stugan är bokad eller inte
    Booking.find({advert: advertId}, function(err, advert) {

        if(err) {
            res.status(500).json({
                message: err
            })
        }

        // loopar genom annonserna i bookings
        for(var i = 0; advert.length > i; i++) {
            /* mellanvariabel för de annonser vi hittar
                som vi använder för att jämföra 
             användarens input och tillgänglighet */
            
            const bookingsFrom =    new Date(advert[i].from);
            const bookingsTo =      new Date(advert[i].to);

            // checka om stugan är tillgänglig för önskad bokning
            if(bookingsFrom >= from && bookingsTo <= to) {
                res.status(409).json({
                    message: 'booking not available for the date provided'
                })
                return;
            } 
        }
        
        // den första och sista dagen en bokning är möjlig emellan
        const fromMin = new Date(advertId.from);
        const toMax =   new Date(advertId.to);

        if(from < fromMin || to > toMax) {
            res.status(409).json({
                message: 'leasing not possible for provided date'
            });

        } else {
            booking.save()
                .then(result => {
                    console.log(result);
                    res.status(201).json({
                        message: 'Booking successfully created',
                        booking: booking
                    });
                })
                .catch(error => {
                    console.log(error);
                    const err = new Error(error);
                    err.status = error.status || 500;
                    
                    next(err)
                });
            };
        });
    });
});

router.patch('/:id', checkAuth, (req, res, next) => {
    Booking.update({ _id: req.params.id}, {$set: { cabin: req.body.cabin, from: req.body.from, to: req.body.to }})
        .exec()
        .then(result => {
            res.status(200).json({
                message: 'Booking updated successfully'
            })
        })
        .catch(error => {
            console.log(error);
            const err = new Error(error);
            err.status = error.status || 500;
            
            next(err) // bollar vidare error-meddelandet till nästa middleware i felhanteringen
        });
});

router.delete('/:id', checkAuth, (req, res, next) => {
    Booking.remove({_id: req.params.id}).exec()
        .then(result => {
            res.status(200).json({
                message: 'Booking deleted successfully'
            });
        })
        .catch();
});

//kom ihåg att göra våra nödvändiga exporteringar, nu är variabeln router exporterbar 
module.exports = router;