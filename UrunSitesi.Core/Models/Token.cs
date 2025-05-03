namespace UrunSitesi.Core.Models
{
    public class Token
    {
        public string AccessToken { get; set; } // apiye ulaşım token ı
        public DateTime Expiration { get; set; } // token yaşam süresi için
        public string RefreshToken { get; set; } // süresi biten token ı yenilemek için gerekli kullanıcı guid değerini tutacağımız prop
    }
}
