/*
DET SOM HÄNDER NU ATT OM VI SURFAR IN LOKALT PÅ PORT 8080 (localhost:8080)
SÅ SKICKAR VI FÖRFRÅGAN ÅT OSS SJÄLVA OCH FÅR SVAR TILLBAKS

Väldigt simpel REST API exempel
*/

const http = require('http');

const port = 8080;

const app = require('./app') // importera ett bibliotek vi själva skapat, i detta fall "app.js"
const server = http.createServer(app); // som parameter till createServer() vem som ska ta hand om meddelandet är vår app.js

server.listen(port); // startar servern och lyssnar på port 8080
