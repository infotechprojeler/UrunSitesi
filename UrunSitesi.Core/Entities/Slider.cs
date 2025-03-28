﻿using System.ComponentModel.DataAnnotations;

namespace UrunSitesi.Core.Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), StringLength(150)]
        public string? Title { get; set; }
        [Display(Name = "Açıklama"), StringLength(500)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
    }
}
