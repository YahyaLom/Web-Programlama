using HastaneOtomasyonASP.NET.Utility;
using Microsoft.EntityFrameworkCore;

namespace HastaneOtomasyonASP.NET.Models
{
	public class DoktorRepository : Repository<Doktor>, IDoktorRepository
	{
		private UygulamaDbContext _uygulamaDbContext;

		public DoktorRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;

		}
		//DOKTOR CALISMA SAATLERI
		public ICollection<CalismaSaati> GetDoktorCalismaSaatleri(int doktorId)
		{
			return _uygulamaDbContext.Doktorlar.Include(d => d.CalismaSaatleri).FirstOrDefault(d => d.Id == doktorId)?.CalismaSaatleri;
		}

		public void Guncelle(Doktor doktor)
		{
			_uygulamaDbContext.Update(doktor);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
