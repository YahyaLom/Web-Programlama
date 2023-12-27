using System.ComponentModel.DataAnnotations;

namespace HastaneOtomasyonASP.NET.Models
{
	public class Hasta
	{

		public int Id { get; set; }
		[Required]
		public string Ad { get; set; }
		[Required]
		public string Soyad { get; set; }
		[Required]
		public string Telefon { get; set; }

		public string Aciklama { get; set; }	

		
		
		
	}
}
