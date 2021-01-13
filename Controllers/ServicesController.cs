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
    [Route("services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            try { 

                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                IEnumerable<Service> services = sql.Query<Service>("SELECT * FROM Services;");
                sql.Close();

                return StatusCode(200, services);
            } 

            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        public IActionResult Post([FromBody] Service service)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "INSERT INTO Services (serviceName) values (@serviceName);";

                sql.Query(Query, service);
                sql.Close();

                return StatusCode(201, service);

            }

            catch(Exception e) 
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Service service)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Server=tcp:chroomdbsrv.database.windows.net,1433;Initial Catalog=roombookingdb;Persist Security Info=False;User ID=test;Password=Arcada123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                sql.Open();

                string Query = "UPDATE Services SET ServiceName = @serviceName WHERE id = " + id + ";";
                sql.Query(Query, service);
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

                string Query = "DELETE FROM Services WHERE id = " + id;
                sql.Query(Query);

                sql.Close();

                return StatusCode(200, "Service #" + id + " deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
