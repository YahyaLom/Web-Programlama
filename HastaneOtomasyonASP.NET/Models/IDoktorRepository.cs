namespace HastaneOtomasyonASP.NET.Models
{
	public interface IDoktorRepository:IRepository<Doktor>
	{
		void Guncelle(Doktor doktor);
		void Kaydet();
		ICollection<CalismaSaati> GetDoktorCalismaSaatleri(int doktorId);//DOKTOR CALISMA SAATLERI GET
	}
}
