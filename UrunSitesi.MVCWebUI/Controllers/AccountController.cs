﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Models;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public AccountController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                    if (kullanici != null)
                    {
                        ModelState.AddModelError("", "Bu Email ile Daha Önce Kayıt Olunmuş!");
                    }
                    else
                    {
                        user.IsActive = true;
                        user.IsAdmin = false;
                        user.CreateDate = DateTime.Now;
                        await _dbContext.Users.AddAsync(user);
                        await _dbContext.SaveChangesAsync();
                        TempData["Message"] = @$"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Başarılı!</strong> Üye Kaydınız Başarıyla Tamamlanmıştır. Üye Girişi Yapabilirsiniz.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        return RedirectToAction("Login");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = await _dbContext.Users.FirstOrDefaultAsync(x => x.IsActive && x.Email == model.Email && x.Password == model.Password);
                    if (kullanici != null)
                    {
                        var haklar = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, kullanici.Name),
                            new Claim(ClaimTypes.Email, kullanici.Email),
                            new Claim(ClaimTypes.UserData, kullanici.UserGuid.ToString()),
                            new Claim(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User")
                        };
                        var kullaniciKimligi = new ClaimsIdentity(haklar, ClaimsIdentity.DefaultNameClaimType);
                        var claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                        await HttpContext.SignInAsync(claimsPrincipal); // yukarıdaki tüm ayarlarla beraber sisteme girişi yap
                        return Redirect(!string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/Account/Index");
                    }
                    else
                        ModelState.AddModelError("", "Giriş Başarısız!");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
