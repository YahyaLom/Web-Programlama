namespace HastaneOtomasyonASP.NET.Models
{
	public interface IPolikinlikRepository:IRepository<Polikinlik>
	{
		void Guncelle(Polikinlik polikinlik);
		void Kaydet();
	}
}
