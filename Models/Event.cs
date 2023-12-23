namespace Adventure_MVC.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
