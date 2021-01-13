using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RoomBookingApp
{
    class RestApi
    {
        // måste definiera funktionen som en asynchronous också, så att vi kan fortsätta göra annat fast vi väntar på vår response
        // vi vill alltså att koden vi har här inne ska kunna köras parallelt med allt annat
        // OCH eftersom vi vill returnera något i en async funktion så måste vi returnera det som en Task med vårt Listobjekt
        public async Task<List<Room>> GetRooms()
        {
            //Skicka GET request till /rooms
            HttpClient client = new HttpClient();

            // om jag utför en async funktion som ska köra i bakgrunden, måste vi köra med
            // nyckelordet await, så att körningen körs i bakgrunden och inte fryser sig
            // körs i princip i en bakgrundstråd
            HttpResponseMessage response = await client.GetAsync("http://localhost:63501/rooms");

            // hela HTTP-paketet inbakat i "response", bryta ner det paket till det vi vill ha
            HttpContent content = response.Content;

            // från content vill vi läsa innehållet som JSON-objekt, obs await nyckelord
            string jsonObject = await content.ReadAsStringAsync();

            // deserialisera jsonObj till en lista av rum som vi returnerar sen
            JavaScriptSerializer js = new JavaScriptSerializer();


            return js.Deserialize<List<Room>>(jsonObject);
        }

        public async Task SaveRoom(Room room)
        {
            HttpClient client = new HttpClient();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonObj = js.Serialize(room);

            StringContent body = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            await client.PostAsync("http://localhost:63501/rooms", body);
        }

        public async Task DeleteRoom(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:63501/rooms/" + id);
        }

        public async Task<List<Booking>> GetBookings()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:63501/bookings");
            HttpContent content = response.Content;
            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Deserialize<List<Booking>>(jsonObject);
        }

        public async Task SaveBooking(Booking booking)
        {
            HttpClient client = new HttpClient();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonObj = js.Serialize(booking);

            StringContent body = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            await client.PostAsync("http://localhost:63501/bookings", body);
        }

        public async Task DeleteBooking(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:63501/bookings/" + id);
        }


        public async Task<List<Service>> GetServices()
        {

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://roombookingapp.azurewebsites.net/services");

            HttpContent content = response.Content;

            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Deserialize<List<Service>>(jsonObject);
        }

        public async Task<List<Cabin>> GetCabins()
        {

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://roombookingsystem.azurewebsites.net/cabins");

            HttpContent content = response.Content;

            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Deserialize<List<Cabin>>(jsonObject);
        }

        public async Task<List<Cabin>> GetCabinById(string userId)
        {

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://roombookingapp.azurewebsites.net/cabins/" + userId);

            HttpContent content = response.Content;

            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Deserialize<List<Cabin>>(jsonObject);
        }

        public async Task<List<Reservation>> GetReservations()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://roombookingapp.azurewebsites.net/reservations");
            HttpContent content = response.Content;
            string jsonObject = await content.ReadAsStringAsync();

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Deserialize<List<Reservation>>(jsonObject);
        }
        public async Task ModifyReservation(int id, Reservation reservation)
        {
            HttpClient client = new HttpClient();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonObj = js.Serialize(reservation);

            StringContent body = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            await client.PutAsync("http://roombookingapp.azurewebsites.net/reservations/" + id, body);
        }
        public async Task SaveReservation(Reservation reservation)
        {
            HttpClient client = new HttpClient();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonObj = js.Serialize(reservation);

            StringContent body = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            await client.PostAsync("http://roombookingapp.azurewebsites.net/reservations", body);
        }

        public async Task DeleteReservation(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://roombookingapp.azurewebsites.net/reservations/" + id);
        }
    }
}
