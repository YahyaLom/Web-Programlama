using HastaneOtomasyonASP.NET.Utility;

namespace HastaneOtomasyonASP.NET.Models
{
	public class HastaRepository : Repository<Hasta>, IHastaRepository
	{
		private  UygulamaDbContext _uygulamaDbContext;

		public HastaRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;

		}


		public void Guncelle(Hasta hasta)	
		{
			_uygulamaDbContext.Update(hasta);
		}

		
		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
