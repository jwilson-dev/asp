namespace asp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public User User {get; set;}
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Region { get; set; }
        public int Age { get; set; }
    }
}