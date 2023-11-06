

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAspApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display order for category must me greater than 0")]
        public int DisplayOrder { get; set; }

    }
}
