const mongoose = require('mongoose');
const user = require('./user');

// datanamn för vår Collection med respektive datatyper
const cabinSchema = mongoose.Schema({
    _id: mongoose.Types.ObjectId, // speciell datatyp, mongoose specifik "id"
    owner:  { type: String, required: true },
    adress: { type: String, required: true },
    m2:     { type: Number, required: true },
    sauna:  { type: String, required: true },
    beach:  { type: String, required: true }
});

module.exports = mongoose.model('Cabin', cabinSchema);