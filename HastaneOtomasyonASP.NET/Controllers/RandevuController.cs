using HastaneOtomasyonASP.NET.Migrations;
using HastaneOtomasyonASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace HastaneOtomasyonASP.NET.Controllers
{
	public class RandevuController : Controller
	{
		private readonly IRandevuRepository _randevuRepository;
		private readonly IHastaRepository _hastaRepository;
		private readonly IDoktorRepository _doktorRepository;
		private readonly IPolikinlikRepository _polikinlikRepository;


		public RandevuController(IRandevuRepository randevu,IHastaRepository hasta,IDoktorRepository doktor,IPolikinlikRepository polikinlik)
		{
			_randevuRepository = randevu;
			_hastaRepository=hasta;
			_doktorRepository = doktor;
			_polikinlikRepository= polikinlik;
		}
		[Authorize(Roles = "Admin,Hasta")]
		public IActionResult Index()
		{
			//List<Randevu> objRandevuList = _randevuRepository.GetAll().ToList();//veritabanına _uygulamadbcontex ile baglanıp  listesi alıyoruz.
			List<Randevu> objRandevuList = _randevuRepository.GetAll(includeProps:"Hasta,Doktor,Polikinlik").ToList();
			return View(objRandevuList);//view'ev  Listesi gönderiyoruz.
		}
		[Authorize(Roles = "Admin,Hasta")]
		public IActionResult Ekle()
		{
			//HASTALAR
			IEnumerable<SelectListItem> HastaList = _hastaRepository.GetAll().Select(h => new SelectListItem //hasta  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = h.Ad,//hasta ad
				Value = h.Id.ToString(),//hasta id

			});
			ViewBag.HastaList = HastaList;
			//DOKTORLAR
			IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().Select(d => new SelectListItem //doktor  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = d.Ad,//doktor ad
				Value = d.Id.ToString(),//doktor id

			});
			ViewBag.DoktorList = DoktorList;

			//POLİKİNLİKLER
			IEnumerable<SelectListItem> PolikinlikList = _polikinlikRepository.GetAll().Select(p => new SelectListItem //doktoralan  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = p.Adres,//polikinlik adres
				Value = p.Id.ToString(),//polikinlik id

			});
			ViewBag.PolikinlikList = PolikinlikList;


			IEnumerable<SelectListItem> DoktorCalismaSaatList = _doktorRepository.GetAll().Select(d => new SelectListItem
			{                                                                                                   //drop-down şeklinde	
				Text = (d.CalismaSaatleri).ToString(),
				Value = d.Id.ToString()

			}) ;
			ViewBag.DoktorCalismaSaatList = DoktorCalismaSaatList;

			return View();
		}

		[Authorize(Roles = "Admin,Hasta")]
		[HttpPost]
		public IActionResult Ekle(Randevu randevu)
		{
			if (ModelState.IsValid)
			{
				_randevuRepository.Ekle(randevu);
				_randevuRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = "Yeni Randevu Randevu Listesine başarıyla Eklendi.";
				return RedirectToAction("Index", "Randevu");//Randevu controller'ın Index metodunu cagirir
			}
			return View();
		
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Guncelle(int? id)
		{
			#region VeriCekme


			//HASTALAR
			IEnumerable<SelectListItem> HastaList = _hastaRepository.GetAll().Select(h => new SelectListItem //hasta  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = h.Ad,//hasta ad
				Value = h.Id.ToString(),//hasta id

			});
			ViewBag.HastaList = HastaList;
			//DOKTORLAR
			IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().Select(d => new SelectListItem //doktor  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = d.Ad,//doktor ad
				Value = d.Id.ToString(),//doktor id

			});
			ViewBag.DoktorList = DoktorList;

			//POLİKİNLİKLER
			IEnumerable<SelectListItem> PolikinlikList = _polikinlikRepository.GetAll().Select(p => new SelectListItem //doktoralan  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = p.Adres,//polikinlik adres
				Value = p.Id.ToString(),//polikinlik id

			});
			ViewBag.PolikinlikList = PolikinlikList;


			IEnumerable<SelectListItem> DoktorCalismaSaatList = _doktorRepository.GetAll().Select(d => new SelectListItem
			{                                                                                                   //drop-down şeklinde	
				Text = (d.CalismaSaatleri).ToString(),
				Value = d.Id.ToString()

			});
			ViewBag.DoktorCalismaSaatList = DoktorCalismaSaatList;

			#endregion

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Randevu? randevuVt = _randevuRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (randevuVt == null)
			{
				return NotFound();
			}
			return View(randevuVt);//doktorVt nesnemizi view'e gönderdik
		}

		[HttpPost]
		public IActionResult Guncelle(Randevu randevu)
		{
			if (ModelState.IsValid)
			{
				_randevuRepository.Guncelle(randevu);
				_randevuRepository.Kaydet();
				TempData["basarili"] = "Güncelleme işlemi başarılı.";
				return RedirectToAction("Index");
			}
			return View();
		}







		[Authorize(Roles = "Admin")]
		public IActionResult Sil(int? id)//index.cshtml asp-route-id=@doktor.Id ile id degeri alıyoruz
		{
			#region VeriCekme


			//HASTALAR
			IEnumerable<SelectListItem> HastaList = _hastaRepository.GetAll().Select(h => new SelectListItem //hasta  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = h.Ad,//hasta ad
				Value = h.Id.ToString(),//hasta id

			});
			ViewBag.HastaList = HastaList;
			//DOKTORLAR
			IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().Select(d => new SelectListItem //doktor  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = d.Ad,//doktor ad
				Value = d.Id.ToString(),//doktor id

			});
			ViewBag.DoktorList = DoktorList;

			//POLİKİNLİKLER
			IEnumerable<SelectListItem> PolikinlikList = _polikinlikRepository.GetAll().Select(p => new SelectListItem //doktoralan  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = p.Adres,//polikinlik adres
				Value = p.Id.ToString(),//polikinlik id

			});
			ViewBag.PolikinlikList = PolikinlikList;


			IEnumerable<SelectListItem> DoktorCalismaSaatList = _doktorRepository.GetAll().Select(d => new SelectListItem
			{                                                                                                   //drop-down şeklinde	
				Text = (d.CalismaSaatleri).ToString(),
				Value = d.Id.ToString()

			});
			ViewBag.DoktorCalismaSaatList = DoktorCalismaSaatList;

			#endregion



			if (id == null || id == 0)
			{
				return NotFound();
			}
			Randevu? randevuVT = _randevuRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (randevuVT == null)
			{
				return NotFound();
			}
			return View(randevuVT);//doktorVt nesnemizi view'e gönderdik
		}
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Randevu? randevu = _randevuRepository.Get(u => u.Id == id);
			if (randevu == null) { return NotFound(); }
			_randevuRepository.Sil(randevu);//sil
			_randevuRepository.Kaydet();//kaydet
			TempData["basarili"] = "Silme işlemi başarılı.";//kullaniciya mesaj
			return RedirectToAction("Index");//listele
		}



		//CALISMASAATLERI DKTOR
		public JsonResult LoadState(int doktorId)
		{
			var calismaSaatleri=_doktorRepository.GetDoktorCalismaSaatleri(doktorId);
			return Json(calismaSaatleri);
		}

	}
}
