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
        //Validasyon yapmak istedi�imiz alanlar
        ValidateAudience = true, // Kitleyi Do�rula
        ValidateIssuer = true, // Token� vereni do�rula
        ValidateLifetime = true, // Token ya�am s�resini do�rula
        ValidateIssuerSigningKey = true, // Token� verenin imzalama anahtarini Do�rula
        ValidIssuer = builder.Configuration["Token:Issuer"], // Token� veren saglayici
        ValidAudience = builder.Configuration["Token:Audience"], // Token� kullanacak kullanici
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), // Token� imzalama Anahtari
        ClockSkew = TimeSpan.Zero // saat fark� olmas�n
    };
});

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy
            .AllowAnyOrigin() // t�m�ne izin ver
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
