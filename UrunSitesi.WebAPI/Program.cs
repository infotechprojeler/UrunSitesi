using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrunSitesi.Data;
using UrunSitesi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        // Token = Jeton
        //Validasyon yapmak istediðimiz alanlar
        ValidateAudience = true, // Kitleyi Doðrula
        ValidateIssuer = true, // Tokený vereni doðrula
        ValidateLifetime = true, // Token yaþam süresini doðrula
        ValidateIssuerSigningKey = true, // Tokený verenin imzalama anahtarini Doðrula
        ValidIssuer = builder.Configuration["Token:Issuer"], // Tokený veren saglayici
        ValidAudience = builder.Configuration["Token:Audience"], // Tokený kullanacak kullanici
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), // Tokený imzalama Anahtari
        ClockSkew = TimeSpan.Zero // saat farký olmasýn
    };
});

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy
            .AllowAnyOrigin() // tümüne izin ver
                              // .WithOrigins("https://localhost:7262") // sadece bu domainlere izin ver
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// UseCors
app.UseCors("default");

app.Run();
