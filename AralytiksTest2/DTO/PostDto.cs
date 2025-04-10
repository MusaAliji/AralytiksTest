using System.ComponentModel.DataAnnotations;

namespace AralytiksTest2.DTO
{
    public class PostDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
    }
}
