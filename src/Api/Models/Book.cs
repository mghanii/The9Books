namespace The9Books.Models
{
    public class Book
    {
        public string Id { get; }
        public string NameAr { get; }
        public string NameEn { get; }
        public int HadithCount { get; }

        public Book(string id, string nameAr, string nameEn, int hadithCount)
        {
            Id = id;
            NameAr = nameAr;
            NameEn = nameEn;
            HadithCount = hadithCount;
        }
    }
}