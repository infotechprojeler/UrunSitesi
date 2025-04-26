using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UrunSitesi.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), StringLength(50), Required(ErrorMessage = "Kategori Adı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }
        [JsonIgnore]
        public IList<Product>? Products { get; set; }
    }
}
