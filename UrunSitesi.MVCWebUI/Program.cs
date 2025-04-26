using Microsoft.AspNetCore.Authentication.Cookies;
using UrunSitesi.Data;
using UrunSitesi.Service; // Service using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.Cookie.Name = "UrunSitesi"; // olu�acak cookie nin ismi UrunSitesi olsun
});

// 3 farkl� servis ekleme y�ntemi var
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // uygulamaya IService i�in bir nesne talebi gelirse ona Service s�n�f�ndan bir nesne olu�turup g�nder.
// builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // nesne i�in gelen her istekte yeni bir �rnek olu�turur ayn� kullan�c� i�in ayn� �rne�i kullan�r
// builder.Services.AddSingleton(typeof(IService<>), typeof(Service<>)); // nesne i�in uygulamada 1 tane �rnek olu�turur ve hep onu kullan�r

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();