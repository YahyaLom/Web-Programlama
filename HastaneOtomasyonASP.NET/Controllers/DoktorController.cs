using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HastaneOtomasyonASP.NET.Controllers
{
	public class DoktorController : Controller
	{
		private readonly IDoktorRepository _doktorRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;

		public DoktorController(IDoktorRepository context, IWebHostEnvironment webHostEnvironment)
		{

			_doktorRepository = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Doktor>objDoktorList= _doktorRepository.GetAll().ToList();//veritabanına _uygulamadbcontex ile baglanıp doktorlar listesi alıyoruz.
			return View(objDoktorList);//view'ev Dokorlar Listesi gönderiyoruz.
		}
		
		public IActionResult EkleGuncelle(int? id)
		{
			//comboox hasta id seçiliyo
			IEnumerable<SelectListItem>  DoktorList = _doktorRepository.GetAll().Select(k => new SelectListItem
			{
				Text = k.Ad,
				Value = k.Id.ToString()

			}
			);

			ViewBag.DoktorList = DoktorList;
			if (id == null || id == 0)
			{
				//ekleme
				return View();
			}
			else
			{
				//guncelleme

				Doktor? DoktorVt = _doktorRepository.Get(u => u.Id == id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
				if (DoktorVt == null)
				{
					return NotFound();
				}
				return View(DoktorVt);//doktorVt nesnemizi view'e gönderdik
			}

		}

		

		//formdan verileri http post ile alıyoruz ve buraya veriler geliyor
		//veriler Doktor turunden nesne
		[HttpPost]
		public IActionResult EkleGuncelle(Doktor doktor,IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwRootPath = _webHostEnvironment.WebRootPath;
				string doktorPath = Path.Combine(wwRootPath, @"img");
				if (file != null) 
				{

					using (var fileStream = new FileStream(Path.Combine(doktorPath, file.FileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					doktor.ResimURL = @"\img\" + file.FileName;
				}

				if (doktor.Id == 0)
				{
					_doktorRepository.Ekle(doktor);
					TempData["basarili"] = "yeni doktor listeye başarıyla eklendi.";//kullanıcı mesaj

				}
				else
				{
					_doktorRepository.Guncelle(doktor);
					TempData["basarili"] = "güncelleme işlemi başarılı.";//kullanıcı mesaj

				}
				_doktorRepository.Kaydet();
				return RedirectToAction("Index");//Listele geri donuyor.

			}
			return View();
			
		}

		


		public IActionResult Sil(int? id)//index.cshtml asp-route-id=@doktor.Id ile id degeri alıyoruz
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Doktor? doktorVt = _doktorRepository.Get(u=>u.Id==id);//uygulamadbcontex veri tabanina gidip doktorlar tablosundan id degerine göre buluyor
			if (doktorVt == null)
			{
				return NotFound();
			}
			return View(doktorVt);//doktorVt nesnemizi view'e gönderdik
		}
		[HttpPost,ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Doktor? doktor = _doktorRepository.Get(u => u.Id == id);
			if (doktor==null) { return NotFound(); }
			_doktorRepository.Sil(doktor);//sil
			_doktorRepository.Kaydet();//kaydet
			TempData["basarili"] = "Silme işlemi başarılı.";//kullaniciya mesaj
			return RedirectToAction("Index");//listele
		}


	}
}
