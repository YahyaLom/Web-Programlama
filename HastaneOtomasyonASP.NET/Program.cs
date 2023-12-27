using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//dependency injection islemleri
// Add services to the container.
builder.Services.AddControllersWithViews();
//uygulama Kopru kurduk veri tabaný ile
builder.Services.AddDbContext<UygulamaDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))) ;
//DoktorRepository Oluþturmaya yardýcý oluyor
builder.Services.AddScoped<IDoktorRepository, DoktorRepository>();
//HastaRepository Oluþturmaya yardýcý oluyor
builder.Services.AddScoped<IHastaRepository, HastaRepository>();
//RandevuRepo Oluþturmaya yardýcý oluyor
builder.Services.AddScoped<IRandevuRepository,RandevuRepository>();
//RandevuRepo Oluþturmaya yardýcý oluyor
builder.Services.AddScoped<IPolikinlikRepository, PolikinlikRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
