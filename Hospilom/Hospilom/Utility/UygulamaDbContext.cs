using Hospilom.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospilom.Utility
{
    public class UygulamaDbContext : DbContext
    {

        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) {}
        public DbSet<DoktorTuru> DoktorTurleri { get; set; }
        //database hakında değişiklik yaptıktan sonra
        //paket yönetici konsoluna add-migration yazılıp enterlenecek
        //update-database yaptıktan sonra database te görünür
    }
}
