namespace asp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailURL { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public Developer Developer { get; set; }
        public int DeveloperId { get; set; }
        public bool IsSupended { get; set; }
        public int NumberPlayed { get; set; }
    }
}