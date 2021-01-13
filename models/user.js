const mongoose = require('mongoose');

// datanamn för vår Collection med respektive datatyper
const userSchema = mongoose.Schema({
    _id: mongoose.Types.ObjectId, // speciell datatyp, mongoose specifik "id"
    firstname:  { type: String, required: true },
    lastname:   { type: String, required: true },
    email:      { type: String, required: true },
    password:   { type: String, required: true }
    
});

module.exports = mongoose.model('User', userSchema);