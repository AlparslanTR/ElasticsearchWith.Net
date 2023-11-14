using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogUI.Dtos
{
    public class AddBlogDto
    {
        [Required]
        [Display(Name ="Başlık")]
        public string Title { get; set; } = null!;
        [Required]
        [Display(Name = "İçerik")]
        public string Content { get; set; } = null!;
        [Display(Name = "Tag")]
        public List <string> Tags { get; set; } = new();

    }
}
