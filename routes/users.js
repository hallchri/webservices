const express = require('express');
const mongoose = require('mongoose');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken')
const router = express.Router();
const User = require('../models/user');
const { route, use } = require('./cabins');

// POST för user signup och korrekt lagring av hemlig info
router.post('/signup', (req, res, next) => {
    var user = new User();
        bcrypt.hash(req.body.password, 10, (err, hash) => {
            if(err) {
                res.status(500).json({
                    message: err
                });
            }
            // om vi lyckas skapa hashen
            else {
                    user = new User({
                    _id: new mongoose.Types.ObjectId(),
                    email: req.body.email,
                    firstname: req.body.firstname,
                    lastname: req.body.lastname,
                    password: hash
                });
            }
            //Om vi hittar en användare med den email vi försöker registrera 
            User.find({ email: user.email }, function(err, email) {
                if (err) {
                    res.status(500).json({
                        message: err
                    })
                }
                if (email.length!=0) {
                    res.status(409).json({
                        message: "the email provided is already tied with an existing account"
                    });                      
                } else {
                    user.save()
                    .then(result => {
                        res.status(201).json({
                            message: 'User registered successfully'
                        })
                    })
                    .catch(); 
                }
        }); 
    });
});

router.post('/login', (req, res, next) => {
// verifiera en användare
// vi vill inte hitta alla användare, utan bara den som är 
// överens med den email man vill logga in med

User.find({ email: req.body.email}).exec()
    .then( user => {

        // om det var fel email så returnera fel
        if(user.length < 1) {
            res.status(401).json({
                message: 'authentication failed'
            });

            // om det ger grönt ljus
        } else {

            // vi jämför ett password, första param password i plain text, andra param ett hashat password
            // user[0] är ju då positionen i json-arrayn som returneras om användare matchas rätt

            bcrypt.compare(req.body.password, user[0].password, (err, result) => {
                if(err) {
                    res.status(401).json({
                        message: 'authentication failed'
                    });
                } 

                else if(result) {
                    // sedan generera en webtoken (JSON Web Token)
                    const token = jwt.sign(
                        {
                        email: user.email,
                        userId: user[0]._id
                        },
                        "secret", // andra parameter för serverns "secret" nyckel, ska int va såhär
                        {expiresIn: "1h"} // tredje är extra params, vi ställer här in tokens livslängd
                    );
                    
                    res.status(200).json({
                        message: 'authentication successful',
                        token: token,
                    });


                } else {
                    res.status(401).json({
                        message: 'authentication failed'
                    })
                }
            });
        }
        
    })
    .catch();
});

module.exports = router;