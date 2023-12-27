using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneOtomasyonASP.NET.Models
{
	public class Randevu
	{
		public int Id { get; set; }//Randevu
		[Required]
		public DateTime Tarih { get; set; }


		public String? Aciklama { get; set; }


		//FOREİGN KEY İLİŞİKSİ
		[ValidateNever]
		public int HastaId { get; set; }
		[ForeignKey("HastaId")]
		[ValidateNever]
		public Hasta Hasta { get; set; }

		//FOREİGN KEY İLİŞİKSİ
		[ValidateNever]
		public int DoktorId { get; set; }
		[ForeignKey("DoktorId")]
		[ValidateNever]
		public Doktor Doktor { get; set; }

		//FOREİGN KEY İLİŞİKSİ
		[ValidateNever]
		public int PolikinlikId { get; set; }
		[ForeignKey("PolikinlikId")]
		[ValidateNever]
		public Polikinlik Polikinlik { get; set; }
		
	}
}
