const express = require('express');
const app = express();
const morgan = require('morgan');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');

// om det kommer ett inkommande meddelande så är det här vi "hanterar det", en request listener
/*app.use((req, res, next) => {
    //svaret här tillbaka till klienten
    res.status(200).json( {
        message: 'Request received' // json-meddelande
    });
});
*/

//Router exempel
const cabinRoutes =     require('./routes/cabins');
const bookingRoutes =   require('./routes/bookings');
const advertRoutes =    require('./routes/adverts');
const userRoutes =      require('./routes/users');

// Sätter upp förbindelsen till databasen
mongoose.set('useNewUrlParser', true);
mongoose.set('useFindAndModify', false);
mongoose.set('useCreateIndex', true);
mongoose.set('useUnifiedTopology', true);
mongoose.connect('mongodb+srv://test:test@roombookingsystem.6jl23.mongodb.net/roomBokingSystem?retryWrites=true&w=majority');

// Alla inkommande requests loggas i konsolen med paketet 'morgan'
app.use(morgan('dev'));

app.use(bodyParser.urlencoded({extended: false }));

//Parse inkommande JSON-objekt automatiskt
app.use(bodyParser.json());

// om det kommer in requests på diverse sidor/länkar så ruttas dom rätt
app.use('/cabins',   cabinRoutes);
app.use('/bookings', bookingRoutes);
app.use('/adverts',  advertRoutes);
app.use('/users',    userRoutes);

// Vi implementerar våra egna informationsmeddelanden
// t.ex. felaktig URL
app.use((req, res, next) => {
    const error = new Error("bad URL"); // skapar nytt/eget errormeddelande
    error.status = 404;
    next(error);    // vi skickar in vår error som parameter i next(), då går vi vidare till vår nästa hanteringsmetod
                    // vilket vi hittar näst under i kodenb
});

// Hantering av alla andra fel
app.use((error, req, res, next) => {
    res.status(error.status || 500).json({
        status: error.status,
        error: error.message
    });
});

// se till att vår app.js är exporterbar
module.exports = app;