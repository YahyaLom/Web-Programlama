using HastaneOtomasyonASP.NET.Utility;

namespace HastaneOtomasyonASP.NET.Models
{
	public class CalismaSaatiRepository : Repository<CalismaSaati>, ICalismaSaatiRepository
	{
		private  UygulamaDbContext _uygulamaDbContext;

		public CalismaSaatiRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;

		}


		public void Guncelle(CalismaSaati calismaSaati)
		{
			_uygulamaDbContext.Update(calismaSaati);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
