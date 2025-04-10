using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AralytiksTest2.Models
{
    public class User : BaseModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required DateOnly Birthdate { get; set; }
        public string? Settings { get; set; }
        [NotMapped]
        public UserSettings? SettingsParsed {
            get
            {
                return string.IsNullOrEmpty(Settings) ? null : JsonSerializer.Deserialize<UserSettings>(Settings);
            }
            set
            {
                Settings = value != null ? JsonSerializer.Serialize(value) : null;
            }
        }
        public ICollection<Post>? Posts { get; set; }
    }
}
