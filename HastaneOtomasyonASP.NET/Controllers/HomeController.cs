using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace HastaneOtomasyonASP.NET.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private  LanguageService _localization;
		public HomeController(ILogger<HomeController> logger,LanguageService localization)
		{
			_logger = logger;
			_localization = localization;
		}

        public async Task<IActionResult> API()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/todos");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var todoList = JsonConvert.DeserializeObject<TodoList>(jsonString);

            var filteredTodos = todoList.todos.Where(todo => todo.id % 2 == 0).ToList();


            return View(filteredTodos);
        }


        public async Task<IActionResult> API2()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/todos");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var todoList = JsonConvert.DeserializeObject<TodoList>(jsonString);

            var filteredTodos = todoList.todos.Where(todo => todo.userId%2==1).ToList();


            return View(filteredTodos);
        }


        public async Task<IActionResult> API3()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/todos");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var todoList = JsonConvert.DeserializeObject<TodoList>(jsonString);



            return View(todoList.todos);
        }
       

        public async Task<IActionResult> API4()
        {
            // EndpointController sınıfından bir örnek oluştur

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:7139/api/Api");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var hastalar = JsonConvert.DeserializeObject<List<Hasta>>(jsonString);

            // LINQ sorgusu ile id değeri çift olan hastaları filtrele
            var idCiftOlanHastalar = hastalar.Where(hasta => hasta.Id % 2 == 0).ToList();

            return View(idCiftOlanHastalar);
        }








        public IActionResult Index()
		{
			ViewBag.Welcome = _localization.Getkey("Welcome").Value;
			var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
			return View();
		}
		//LOCALIZATION
		public IActionResult ChangeLanguage(string culture)
		{
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,

				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
				{
					Expires = DateTimeOffset.UtcNow.AddYears(1)
				});
			return Redirect(Request.Headers["Referer"].ToString());	

		
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}