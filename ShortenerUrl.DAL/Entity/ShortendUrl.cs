using System.ComponentModel.DataAnnotations;

namespace ShortenerUrl.DAL.Entity
{
    public class ShortendUrl
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан URl-адрес")]
        [RegularExpression("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$", ErrorMessage = "Некорректный URL-адрес")]
        public string LongUrl { get; set; } = string.Empty;


        [StringLength(7, MinimumLength = 7, ErrorMessage = "Длина строки должна быть 7 символов")]
        [Required(ErrorMessage = "Не указан сокращенный URl-адрес")]
        public string Code { get; set; } = string.Empty;

        public DateTime DateOfCreation { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Значение должно быть положительным числом")]
        public int NumberOfTransitions { get; set; }
    }
}
