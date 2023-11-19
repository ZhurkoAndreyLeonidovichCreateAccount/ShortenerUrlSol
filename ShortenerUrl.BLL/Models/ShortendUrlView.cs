using System.ComponentModel.DataAnnotations;

namespace ShortenerUrl.BLL.Models
{
    public class ShortendUrlView
    {
        public int Id { get; set; }

        [Display(Name = "URl-адрес")]
        [Required(ErrorMessage = "Не указан URl-адрес")]
        [RegularExpression("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$", ErrorMessage = "Некорректный URL-адрес")]
        public string LongUrl { get; set; } = string.Empty;

        [Display(Name = "Сокращенный URl-адрес")]
        //[StringLength(7, MinimumLength = 7, ErrorMessage = "Длина строки должна быть 7 символов")]
        [Required(ErrorMessage = "Не указан сокращенный URl-адрес")]
        public string ShortUrl { get; set; } = string.Empty;

        [Display(Name = "Дата создания")]
        public DateTime DateOfCreation { get; set; }

        [Display(Name = "Количество переходов")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение должно быть положительным числом")]
        public int NumberOfTransitions { get; set; }

        public string Code { get; set; } = string.Empty;


    }
}
