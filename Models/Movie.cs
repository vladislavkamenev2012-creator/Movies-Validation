using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVC_1.Validation;

namespace MVC_1.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Вкажіть назву фільму.")]
        [StringLength(150, ErrorMessage = "Поле «{0}» не може бути довшим за {1} символів.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Режисер")]
        [Required(ErrorMessage = "Вкажіть режисера.")]
        [StringLength(100, ErrorMessage = "Поле «{0}» не може бути довшим за {1} символів.")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄєҐґ'’.\-\s]+$",
            ErrorMessage = "Поле «{0}» може містити лише літери, пробіли, крапку, дефіс та апостроф.")]
        public string Director { get; set; } = string.Empty;

        [Display(Name = "Жанр")]
        [Required(ErrorMessage = "Вкажіть жанр.")]
        [StringLength(100, ErrorMessage = "Поле «{0}» не може бути довшим за {1} символів.")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄєҐґ,\s\-]+$",
            ErrorMessage = "Поле «{0}» може містити лише літери, коми, пробіли та дефіс.")]
        public string Genre { get; set; } = string.Empty;

        [Display(Name = "Рік випуску")]
        [Required(ErrorMessage = "Вкажіть рік випуску.")]
        [YearRange(1888)]
        public int Year { get; set; }

        // Ім'я файлу формується на сервері, з форми воно не приймається
        [BindNever]
        public string PosterFileName { get; set; } = string.Empty;

        [Display(Name = "Файл постера")]
        [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
        [MaxFileSize(2)]
        [NotMapped]
        public IFormFile? PosterFile { get; set; }

        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Додайте опис фільму.")]
        [StringLength(1000, MinimumLength = 10,
            ErrorMessage = "Поле «{0}» має містити від {2} до {1} символів.")]
        public string Description { get; set; } = string.Empty;
    }
}
