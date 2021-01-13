const mongoose = require('mongoose');

const advertSchema = mongoose.Schema({
    _id:    mongoose.Types.ObjectId,
    cabin: { type : mongoose.Schema.Types.Mixed, ref: 'Cabin', required: true },
    from:  { type: Date, required: true },
    to:    { type: Date, required: true },
    owner: { type: mongoose.Types.ObjectId, required: true }
});

advertSchema.methods.toJSON = function() {
    var obj = this.toObject();
    delete obj.owner;
    return obj;
}

module.exports = mongoose.model('Advert', advertSchema);