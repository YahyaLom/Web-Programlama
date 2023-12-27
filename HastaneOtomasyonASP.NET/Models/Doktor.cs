using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneOtomasyonASP.NET.Models
{
	public class Doktor
	{
        public int Id { get; set; }
		[Required]
		public string Ad { get; set; }
		[Required]
		public string Soyad { get; set; }

		[Required(ErrorMessage="Doktor Alanı Boş Bırakılamaz.")]
		[MaxLength(25)]
		[DisplayName("Doktor Alanı")]
		public string Alan { get; set; }//acılır pencere ile hasta secebilsin
		[ValidateNever]
		public string ResimURL { get; set; }


		//[ValidateNever]//validation yapmasın
		//public int HastaId {  get; set; }
		//[ForeignKey("HastaId")]
		//[ValidateNever]//validation yapmasın
		//public Hasta Hasta { get; set; }

	}
}
