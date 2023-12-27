using HastaneOtomasyonASP.NET.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HastaneOtomasyonASP.NET.Models
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private  UygulamaDbContext _uygulamaDbContext;//nesnemiz
		internal DbSet<T> dbSet;
		public Repository(UygulamaDbContext uygulamaDbContext)
		{

			_uygulamaDbContext = uygulamaDbContext;
			dbSet = _uygulamaDbContext.Set<T>();
			//Foreign key isim alma
			_uygulamaDbContext.Randevular.Include(r => r.Hasta.Ad).Include(r => r.Doktor.Ad).Include(r => r.Polikinlik.Adres);

		}


		public void Ekle(T entity)
		{
			dbSet.Add(entity);
		}

	

		public T Get(Expression<Func<T, bool>> filtre,string? includeProps=null)
		{
			IQueryable<T> sorgu = dbSet;
			sorgu = sorgu.Where(filtre);
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					{
						sorgu = sorgu.Include(includeProp);
					}

				}
				
			
			}
			return sorgu.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					sorgu = sorgu.Include(includeProp);

				}
			}
			return sorgu.ToList();

		}

		public void Sil(T entity)
		{
			dbSet.Remove(entity);
		}

		public void SilAralik(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
