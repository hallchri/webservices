const jwt = require('jsonwebtoken');

module.exports = (req, res, next) => {

    try {
        const decoded = jwt.verify(req.header('authorization'), "secret");
        req.user = decoded;
        console.log(decoded);

        // om vi lyckas verifiera JWT så anropar vi nästa callback funktion (next), betyder
        // i praktiken att vi går vidare till funktionen som sparar användaren i databasen
        next();
    }

    // om vi inte lyckas verifiera JWT
    catch {
        res.status(401).json({
            message: "authentication failed"
        });
    }
};