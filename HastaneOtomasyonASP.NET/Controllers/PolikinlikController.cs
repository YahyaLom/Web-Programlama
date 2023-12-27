using HastaneOtomasyonASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneOtomasyonASP.NET.Controllers
{
	public class PolikinlikController : Controller
	{
		private readonly IPolikinlikRepository _polikinlikRepository;//nesnemiz 

		public PolikinlikController(IPolikinlikRepository polikinlikRepository)//
		{
			_polikinlikRepository = polikinlikRepository;
		}


		public IActionResult Index()//listeleme
		{
			List<Polikinlik>? objPolikinlikList = _polikinlikRepository.GetAll().ToList();
			return View(objPolikinlikList);
		}

		public IActionResult Ekle()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Ekle(Polikinlik polikinlik)
		{
			if (ModelState.IsValid)
			{
				_polikinlikRepository.Ekle(polikinlik);
				_polikinlikRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = "Yeni Polikinlik başarıyla oluşturuldu.";
				return RedirectToAction("Index", "Polikinlik");// controller'ın Index metodunu cagirir
			}
			return View();


		}



		public IActionResult Guncelle(int? id)
		{
			if (id == null || id == 0) return NotFound();//id 0 null kontrolü
			Polikinlik? polikinlik = _polikinlikRepository.Get(u => u.Id == id);//parametre id eşit olan id'yi getir
			if (polikinlik == null)
			{
				return NotFound();
			}
			return View(polikinlik);
		}


		[HttpPost]
		public IActionResult Guncelle(Polikinlik polikinlik)
		{

			if (ModelState.IsValid)
			{
				_polikinlikRepository.Guncelle(polikinlik);
				_polikinlikRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = " Polikinlik başarıyla güncellendi.";
				return RedirectToAction("Index", "Polikinlik");//KiitapTuru controller'ın Index metodunu cagirir
			}
			return View();

		}

		//SİL

		public IActionResult Sil(int? id)
		{
			if (id == null || id == 0) return NotFound();//id 0 veya null kontrolü (asp-route-id index.html )
			Polikinlik? polikinlik = _polikinlikRepository.Get(u => u.Id == id);
			if (polikinlik == null)
			{
				return NotFound();
			}
			return View(polikinlik);
		}


		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Polikinlik? polikinlik = _polikinlikRepository.Get(u => u.Id == id);
			if (polikinlik == null) { return NotFound(); }
			_polikinlikRepository.Sil(polikinlik);
			//_uygulamaDbContext.KitapTurleri.Remove(kitapTuru);
			_polikinlikRepository.Kaydet();
			// _uygulamaDbContext.SaveChanges();
			TempData["basarili"] = "Silme işlemi başarılı.";
			return RedirectToAction("Index", "Polikinlik");

		}

	}
}

