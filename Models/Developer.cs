namespace asp.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyURL { get; set; }
        public string Region { get; set; }
    }
}