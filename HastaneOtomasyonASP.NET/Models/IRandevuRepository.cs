namespace HastaneOtomasyonASP.NET.Models
{
	public interface IRandevuRepository:IRepository<Randevu>
	{
		void Guncelle(Randevu randevu);
		void Kaydet();

		
	}
}
