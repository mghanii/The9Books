using Microsoft.EntityFrameworkCore;
using The9Books.Models;

namespace The9Books
{
    public interface IDBContext
    {
        DbSet<Hadith> Hadiths { get; set; }
    }

    public class SQLiteDBContext : DbContext, IDBContext
    {
        public DbSet<Hadith> Hadiths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Data/SunnahDb.db");
    }
}