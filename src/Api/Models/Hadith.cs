namespace The9Books.Models
{
    public class Hadith
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string HadithText { get; set; }
        public string Tafseel { get; set; }
        public string Book { get; set; }
    }
}