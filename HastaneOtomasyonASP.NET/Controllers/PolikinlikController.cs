using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Utility;
using Microsoft.AspNetCore.Authorization;
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

		[Authorize(Roles =UserRoles.Role_Admin)]
		public IActionResult Ekle()
		{
			return View();
		}
        [Authorize(Roles = UserRoles.Role_Admin)]
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


        [Authorize(Roles = UserRoles.Role_Admin)]
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

        [Authorize(Roles = UserRoles.Role_Admin)]
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
        [Authorize(Roles = UserRoles.Role_Admin)]
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

        [Authorize(Roles = UserRoles.Role_Admin)]
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


		//detay


		public IActionResult Detay(int? id)//index.cshtml asp-route-id=@doktor.Id ile id degeri alıyoruz
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Polikinlik? polikinlikVt = _polikinlikRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (polikinlikVt == null)
			{
				return NotFound();
			}
			return View(polikinlikVt);//doktorVt nesnemizi view'e gönderdik
		}

		[HttpPost, ActionName("Detay")]
		public IActionResult DetayPOST(int? id)
		{
			Polikinlik? polikinlik = _polikinlikRepository.Get(u => u.Id == id);
			if (polikinlik == null) { return NotFound(); }
			_polikinlikRepository.Sil(polikinlik);//sil
			_polikinlikRepository.Kaydet();//kaydet
			TempData["basarili"] = "Silme işlemi başarılı.";//kullaniciya mesaj
			return RedirectToAction("Index");//listele
		}

	}
}

