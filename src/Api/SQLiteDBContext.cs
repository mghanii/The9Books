using Microsoft.EntityFrameworkCore;
using Tasaneef.Models;

namespace Tasaneef
{
    public interface IDBContext
    {
        DbSet<Hadith> Hadiths { get; set; }
    }

    public class SQLiteDBContext : DbContext, IDBContext
    {
        public DbSet<Hadith> Hadiths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Data\SunnahDb.db");
    }
}