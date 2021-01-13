using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRoomBookingApi.Models;

namespace MyRoomBookingApi.Controllers
{
    [Route("reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
  
        [HttpGet]
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
                IEnumerable<Reservation> reservations = sql.Query<Reservation>("SELECT * FROM Reservations");
                sql.Close();

                return StatusCode(200, reservations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            try
            {
                // omvandla vår C# objekt så att vi kan sätta in de i vår SQL-databas
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                // med Dapper kan vi använda @name, de ska matcha datamodellen och vår tabell Rooms
                string Query = "INSERT INTO Reservations (CabinName, ServiceType, ReservationDate, ownerId) values(@CabinName, @ServiceType, @ReservationDate, @ownerId)";

                // andra parametern vilket/vilka dataobjekt vi skickar in till vår db
                sql.Query(Query, reservation);

                sql.Close();

                return StatusCode(201, reservation);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Reservation reservation)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "UPDATE Reservations SET CabinName = @CabinName, ServiceType = @ServiceType, ReservationDate = @ReservationDate, ownerId = @ownerId WHERE id = " + id + ";";
                sql.Query(Query, reservation);
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

                string Query = "DELETE FROM Reservations WHERE id = " + id;
                sql.Query(Query);

                sql.Close();

                return StatusCode(200, "Reservation #" + id + " deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
