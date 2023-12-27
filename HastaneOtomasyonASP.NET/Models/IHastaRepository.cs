namespace HastaneOtomasyonASP.NET.Models
{
	public interface IHastaRepository:IRepository<Hasta>
	{
		void Guncelle(Hasta hasta);
		void Kaydet();
	}
}
