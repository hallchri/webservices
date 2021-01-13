using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRoomBookingApi.Models; // importera i princip alla våra klasser i "Models"

namespace MyRoomBookingApi.Controllers
{
    [Route("rooms")]
    [ApiController]
    public class RoomsController : ControllerBase // extends med ":" i c-sharp
    {

        //route.get() i c-sharp
        [HttpGet]

        // returnera vårt Room-objekt som finns i Models
        // IActionResult returnerar statuskod av requests
        // så alltså returnerar en datatyp av IActionResult
        public IActionResult Get()
        {
            try
            {
                // skapa en koppling till vår SQL-databas på Azure
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                //anropa via SQL objektet query för att utföra en query mot min databas
                // om det lyckas är returvärden en lista av c-sharp objekt,
                // måste vara av typen IEnumerable (en List<> datatyp), vissa specialegenskaper
                IEnumerable<Room> rooms = sql.Query<Room>("SELECT * FROM Rooms");
                sql.Close();

                return StatusCode(200, rooms);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]

        // FromBody indikirerar att från inkommande HTTP-meddelande ska det i bodyn finnas rumsinfo med samma variabler vi definierat i Room-modellen
        public IActionResult Post([FromBody] Room room)
        {
            try
            {
                // omvandla vår C# objekt så att vi kan sätta in de i vår SQL-databas
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                // med Dapper kan vi använda @name, de ska matcha datamodellen och vår tabell Rooms
                string Query = "INSERT INTO Rooms (name, seats, computers) values(@name, @seats, @computers)";

                // andra parametern vilket/vilka dataobjekt vi skickar in till vår db
                sql.Query(Query, room);

                sql.Close();

                return StatusCode(201, room);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody ] Room room)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "UPDATE Rooms SET name = @name, seats = @seats, computers = @computers WHERE id = " + id;
                sql.Query(Query, room);
                sql.Close();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "DELETE FROM Rooms WHERE id = " + id;
                sql.Query(Query);

                sql.Close();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
