using Microsoft.AspNetCore.Authentication.Cookies;
using UrunSitesi.Data;
using UrunSitesi.Service; // Service using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.Cookie.Name = "UrunSitesi"; // oluþacak cookie nin ismi UrunSitesi olsun
});

// 3 farklý servis ekleme yöntemi var
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // uygulamaya IService için bir nesne talebi gelirse ona Service sýnýfýndan bir nesne oluþturup gönder.
// builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // nesne için gelen her istekte yeni bir örnek oluþturur ayný kullanýcý için ayný örneði kullanýr
// builder.Services.AddSingleton(typeof(IService<>), typeof(Service<>)); // nesne için uygulamada 1 tane örnek oluþturur ve hep onu kullanýr

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