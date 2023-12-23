namespace Adventure_MVC.Models
{
    public class UserEvent
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }

    }
}
