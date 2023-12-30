using HastaneOtomasyonASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HastaneOtomasyonASP.NET.Controllers
{
	public class HastaController : Controller
	{

		private readonly IHastaRepository _hastaRepository;

		public HastaController(IHastaRepository context)
		{

			_hastaRepository = context;
		}
		[Authorize(Roles = "Admin,Hasta")]
		public IActionResult Index()
		{
			List<Hasta> objHastaList = _hastaRepository.GetAll().ToList();//veritabanına _uygulamadbcontex ile baglanıp doktorlar listesi alıyoruz.
			return View(objHastaList);//view'ev Hasta Listesi gönderiyoruz.
		}


		[Authorize(Roles = "Admin")]
		public IActionResult Ekle()
		{
			return View();
		}

		//formdan verileri http post ile alıyoruz ve buraya veriler geliyor
		//veriler Doktor turunden nesne
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Ekle(Hasta hasta)
		{
			if (ModelState.IsValid)
			{
				_hastaRepository.Ekle(hasta);//ekleme
				_hastaRepository.Kaydet();//kaydetme
				TempData["basarili"] = "yeni hasta listeye başarıyla eklendi.";//kullanıcı mesaj
				return RedirectToAction("Index");//Listele geri donuyor.

			}
			return View();

		}

		[Authorize(Roles = "Admin")]
		public IActionResult Guncelle(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Hasta? hastaVT = _hastaRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (hastaVT == null)
			{
				return NotFound();
			}
			return View(hastaVT);//doktorVt nesnemizi view'e gönderdik
		}

		[HttpPost]
		public IActionResult Guncelle(Hasta hasta)
		{
			if (ModelState.IsValid)
			{
				_hastaRepository.Guncelle(hasta);
				_hastaRepository.Kaydet();
				TempData["basarili"] = "Güncelleme işlemi başarılı.";
				return RedirectToAction("Index");
			}
			return View();
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Sil(int? id)//index.cshtml asp-route-id=@doktor.Id ile id degeri alıyoruz
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Hasta? hastaVt = _hastaRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (hastaVt == null)
			{
				return NotFound();
			}
			return View(hastaVt);//doktorVt nesnemizi view'e gönderdik
		}
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Hasta? hasta = _hastaRepository.Get(u => u.Id == id);
			if (hasta == null) { return NotFound(); }
			_hastaRepository.Sil(hasta);//sil
			_hastaRepository.Kaydet();//kaydet
			TempData["basarili"] = "Silme işlemi başarılı.";//kullaniciya mesaj
			return RedirectToAction("Index");//listele
		}



	}
}
