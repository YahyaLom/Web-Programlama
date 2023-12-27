using HastaneOtomasyonASP.NET.Utility;

namespace HastaneOtomasyonASP.NET.Models
{
	public class PolikinlikRepository : Repository<Polikinlik>, IPolikinlikRepository
	{
		private  UygulamaDbContext _uygulamaDbContext;

		public PolikinlikRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;

		}


		public void Guncelle(Polikinlik polikinlik)
		{
			_uygulamaDbContext.Update(polikinlik);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
