using System.ComponentModel.DataAnnotations;

namespace HastaneOtomasyonASP.NET.Models
{
	public class Polikinlik
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Polikinlik Adı alanı boş bırakılamaz.")]//not null
		[MaxLength(30)]
		public string Ad { get; set; }

		public string Adres { get; set; }
		

	}
}
