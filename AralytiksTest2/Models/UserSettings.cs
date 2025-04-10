namespace AralytiksTest2.Models
{
    public class UserSettings
    {
        public required string Preferences { get; set; }
        public bool ReceiveNotificaitons { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
