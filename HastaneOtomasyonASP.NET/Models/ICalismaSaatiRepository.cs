namespace HastaneOtomasyonASP.NET.Models
{
	public interface ICalismaSaatiRepository : IRepository<CalismaSaati>
	{
		void Guncelle(CalismaSaati calismaSaati);
		void Kaydet();
	}
}
