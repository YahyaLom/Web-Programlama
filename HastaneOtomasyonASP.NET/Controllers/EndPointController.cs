using Microsoft.AspNetCore.Mvc;

namespace HastaneOtomasyonASP.NET.Controllers
{
    public class EndPointController : Controller
    {
        static async Task Getir() 
        {
            await GetStudents();
        }

        static async Task GetStudents() 
        {

            using (HttpClient client =new HttpClient())
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http:localhost:7139/api/Api");
                    response.EnsureSuccessStatusCode();

                    string result =await response.Content.ReadAsStringAsync();
                    Console.WriteLine("GET Response: ");
                    Console.WriteLine(result);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error:{ex.Message}");
                }
        
        }



        public IActionResult Index() 
        {
            return View();
        }


    }
}



