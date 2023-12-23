namespace Adventure_MVC.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }

        // Navigation property for many-to-many relationship with Events
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
