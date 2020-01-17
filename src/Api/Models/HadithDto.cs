namespace Tasaneef.Models
{
    public class HadithDto
    {
        public int Number { get; set; }
        public string Hadith { get; set; }
        public string Tafseel { get; set; }
        public string Book { get; set; }

        public HadithDto(Hadith hadith)
        {
            Number = hadith.Number;
            Hadith = hadith.HadithText;
            Tafseel = hadith.Tafseel;
            Book = hadith.Book;
        }
    }
}