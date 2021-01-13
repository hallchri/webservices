using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRoomBookingApi.Models;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyRoomBookingApi.Controllers
{
    [Route("cabins")]
    [ApiController]
    public class CabinsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try { 
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://roombookingsystem.azurewebsites.net/cabins");
            HttpContent content = response.Content;
            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return StatusCode(200, js.Deserialize<List<Cabin>>(jsonObject));
            }

            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSpecificCabin(string userId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage users = await client.GetAsync("https://roombookingsystem.azurewebsites.net/users");
                HttpResponseMessage cabins = await client.GetAsync("https://roombookingsystem.azurewebsites.net/cabins");

                HttpContent usersContent = users.Content;
                HttpContent cabinsContent = cabins.Content;

                string usersObject = await usersContent.ReadAsStringAsync();
                string cabinsObject = await cabinsContent.ReadAsStringAsync();

                JavaScriptSerializer js = new JavaScriptSerializer();

                // SELFNOTE: Fungerar nu me dehär (<List<User>>)
                var userDetails = JsonConvert.DeserializeObject<List<User>>(usersObject);
                var cabinDetails = JsonConvert.DeserializeObject<List<Cabin>>(cabinsObject);

                Debug.WriteLine("details " + cabinDetails);

                List<Cabin> result = new List<Cabin>();
                
                for (int i = 0; i < cabinDetails.Count; i++)
                {
                    Debug.WriteLine(cabinDetails[i]);
                    if(cabinDetails[i].ownerId == userId)
                    {
                        result.Add(cabinDetails[i]);
                        Debug.WriteLine("resultati " + result);
                    } 
                }

                return StatusCode(200, result);
            }

            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
