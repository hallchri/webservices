const mongoose = require('mongoose');

// datanamn för vår Collection med respektive datatyper
const bookingSchema = mongoose.Schema({
    _id: mongoose.Types.ObjectId, 
    advert:  { type:mongoose.Schema.Types.Mixed, ref: 'Advert', required: true },
    from:   { type: Date, required: true},
    to:     { type: Date, required: true}
});

module.exports = mongoose.model('Booking', bookingSchema);