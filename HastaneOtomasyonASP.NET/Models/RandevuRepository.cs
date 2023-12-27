using HastaneOtomasyonASP.NET.Utility;

namespace HastaneOtomasyonASP.NET.Models
{
	public class RandevuRepository:Repository<Randevu>, IRandevuRepository
	{
		private UygulamaDbContext _uygulamaDbContext;

		public RandevuRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;

		}

		public void Guncelle(Randevu randevu)
		{
			_uygulamaDbContext.Update(randevu);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
