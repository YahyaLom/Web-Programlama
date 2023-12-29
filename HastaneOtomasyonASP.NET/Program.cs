using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Services;
using HastaneOtomasyonASP.NET.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Localizer

builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options=>
	options.DataAnnotationLocalizerProvider = (type, factory)=> 
	{
		var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
		return factory.Create(nameof(SharedResource),assemblyName.Name);

	});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportCultures = new List<CultureInfo>
	{
		new CultureInfo("en-US"),
		new CultureInfo("fr-FR"),
		new CultureInfo("tr-TR"),

	};
	options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
	options.SupportedCultures = supportCultures;
	options.SupportedUICultures = supportCultures;
	options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
	

}
);
#endregion


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
//CalismaSaati Repo Oluþturmaya yardýcý oluyor
builder.Services.AddScoped<ICalismaSaatiRepository, CalismaSaatiRepository>();



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

//LOCALIZATION 
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
