using HastaneOtomasyonASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HastaneOtomasyonASP.NET.Utility
{//TABLOLARI BURAYA EKLEMELİSİN
	public class UygulamaDbContext:DbContext
	{
		public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options): base(options) { }

		public DbSet<Doktor> Doktorlar { get; set; }//olusturulan tablonun adi
		public DbSet<Hasta> Hastalar { get; set; }
		public DbSet<Randevu> Randevular { get; set; }
		public DbSet<Polikinlik> Polikinlikler { get; set; }
		public DbSet<CalismaSaati> CalismaSaatleri { get; set; }



	}
}
