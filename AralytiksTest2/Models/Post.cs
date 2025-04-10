namespace AralytiksTest2.Models
{
    public class Post : BaseModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
        public required string Slug { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
