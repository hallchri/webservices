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
    [Route("bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                IEnumerable<Booking> bookings = sql.Query<Booking>("SELECT * FROM Bookings");
                sql.Close();

                return StatusCode(200, bookings);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "INSERT INTO Bookings (roomId, startDate, endDate) values(@roomId, @startDate, @endDate)";
                sql.Query(Query, booking);

                sql.Close();

                return StatusCode(201, booking);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Booking booking)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "UPDATE Bookings SET roomId = @roomId, startDate = @startDate, endDate = @endDate WHERE id = " + id;
                sql.Query(Query, booking);
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

                string Query = "DELETE FROM Bookings WHERE id = " + id;
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

