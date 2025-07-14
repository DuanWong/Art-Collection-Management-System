
namespace Art_Collection_Manager
{
    public class Artwork
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Medium { get; set; }

        public Artwork(string title, string artist, int year, string medium)
        {
            Title = title;
            Artist = artist;
            Year = year;
            Medium = medium;
        }
    }
}
