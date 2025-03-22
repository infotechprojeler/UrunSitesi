using System.ComponentModel.DataAnnotations;

namespace UrunSitesi.Core.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), Required(ErrorMessage = "Marka Adı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Marka Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Marka Logosu")]
        public string? Logo { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public IList<Product>? Products { get; set; }
    }
}
