namespace API.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string MovieName { get; set; }
        public string Subtitle { get; set; }
        public string Director { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}