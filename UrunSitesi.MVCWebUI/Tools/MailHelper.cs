using UrunSitesi.Core.Entities;
using System.Net; // projeden mail gönderme kütüphanesi
using System.Net.Mail; // projeden mail gönderme kütüphanesi

namespace UrunSitesi.MVCWebUI.Tools
{
    public class MailHelper
    {
        public static async Task<bool> SendMailAsync(Contact contact)
        {
			SmtpClient smtpClient = new SmtpClient("mail.siteadi.com", 587); // 1. parametre mail sunucu adresi, 2. parametre port adresi
            smtpClient.Credentials = new NetworkCredential("mail kullanıcı adımız", "mail şifremiz");
            smtpClient.EnableSsl = false; // mail sunucumuzda ssl varsa true yoksa false yapmalıyız
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com"); // mailin gönderileceği adres
            message.To.Add("bilgi@siteadi.com"); // mesajı alacak mail adresi
            message.Subject = "Siteden 1 Mesaj Geldi!"; // mail konu kısmı
            message.Body = $"İsim: {contact.Name} {contact.Surname} Email: {contact.Email} Telefon: {contact.Phone} Mesaj: {contact.Message}";
            message.IsBodyHtml = true; // mesaj bilgisi html içerikleri desteklesin
            try
			{
                await smtpClient.SendMailAsync(message); // yukardaki ayarlarla maili gönder
                smtpClient.Dispose();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }
    }
}
