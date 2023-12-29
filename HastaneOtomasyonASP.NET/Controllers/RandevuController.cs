using HastaneOtomasyonASP.NET.Migrations;
using HastaneOtomasyonASP.NET.Models;
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

		public IActionResult Index()
		{
			//List<Randevu> objRandevuList = _randevuRepository.GetAll().ToList();//veritabanına _uygulamadbcontex ile baglanıp  listesi alıyoruz.
			List<Randevu> objRandevuList = _randevuRepository.GetAll(includeProps:"Hasta,Doktor,Polikinlik").ToList();
			return View(objRandevuList);//view'ev  Listesi gönderiyoruz.
		}

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

		//CALISMASAATLERI DKTOR
		public JsonResult LoadState(int doktorId)
		{
			var calismaSaatleri=_doktorRepository.GetDoktorCalismaSaatleri(doktorId);
			return Json(calismaSaatleri);
		}

	}
}
