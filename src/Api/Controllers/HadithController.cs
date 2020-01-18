using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The9Books.Models;

namespace The9Books.Controllers
{
    [ApiController]
    [Route("")]
    public class HadithController : ControllerBase
    {
        private readonly IDBContext _dbContext;
        private readonly IRandom _random;
        private static readonly Book[] _books;

        static HadithController()
        {
            _books = new[]
            {
                new Book("bukhari", "صحيح البخاري", "Sahih Bukhari", 7008),
                new Book("muslim", "صحيح مسلم", "Sahih Muslim", 5362),
                new Book("muwataa", "موطأ مالك", "Al Muwatta", 1594),
                new Book("abidawud", "سنن أبي داود", "Sunan Abu Dawud", 4590),
                new Book("ibnmaja", "سنن ابن ماجة", "Sunan Ibn Maja", 4332),
                new Book("musnad", "مسند أحمد بن حنبل", "Musnad Ahmad ibn Hanbal", 26363),
                new Book("tirmidhi", "سنن الترمذي", "Sunan al Tirmidhi", 3891),
                new Book("nasai", "سنن النسائي", "Sunan al-Nasai", 5662),
                new Book("darimi", "سنن الدارمي", "Sunan al Darimi", 3367),
            };
        }

        public HadithController(IDBContext dbContext, IRandom random)
        {
            _dbContext = dbContext;
            _random = random;
        }

        [Route("books")]
        public async Task<ActionResult> Books() => Ok(_books);

        [Route("{bookId}/{num}")]
        [ProducesResponseType(typeof(HadithDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HadithDto>> Get(string bookId, int num)
        {
            if (string.IsNullOrEmpty(bookId) || num <= 0) return BadRequest();

            var book = _books.FirstOrDefault(x => x.Id.Equals(bookId.Trim(), StringComparison.OrdinalIgnoreCase));

            if (book == null) return NotFound("Invalid bookId");
            if (num > book.HadithCount) return NotFound();

            var hadith = await _dbContext
                .Hadiths
                .FirstOrDefaultAsync(x => x.Book == book.Id && x.Number == num);

            if (hadith == null) return NotFound();

            return Ok(new HadithDto(hadith));
        }

        [Route("{bookId}/{start}/{size}")]
        [ProducesResponseType(typeof(IEnumerable<HadithDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HadithDto>>> List(string bookId, int start, int size)
        {
            if (string.IsNullOrEmpty(bookId)) return BadRequest();

            var book = _books.FirstOrDefault(x => x.Id.Equals(bookId.Trim(), StringComparison.OrdinalIgnoreCase));

            if (book == null) return NotFound("Invalid bookId");
            if (start > book.HadithCount) return NotFound();

            var maxSize = 50;

            start = start <= 0 ? 1 : start;
            size = (size <= 0 || size > maxSize) ? maxSize : size;

            var hadiths = await _dbContext.Hadiths
                .Where(x => x.Book == book.Id)
                .OrderBy(x => x.Number)
                .Skip(start - 1)
                .Take(size)
                .ToListAsync();

            return Ok(hadiths.Select(x => new HadithDto(x)));
        }

        [Route("random/{bookId?}")]
        [ProducesResponseType(typeof(HadithDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HadithDto>> Random(string bookId = "bukhari")
        {
            var book = _books.FirstOrDefault(x => x.Id.Equals(bookId.Trim(), StringComparison.OrdinalIgnoreCase));

            if (book == null) return NotFound("Invalid bookId");

            var hadithNumber = _random.RandPositive(book.HadithCount);

            var hadith = await _dbContext
                .Hadiths
                .FirstOrDefaultAsync(x => x.Book == book.Id && x.Number == hadithNumber);

            return Ok(new HadithDto(hadith));
        }
    }
}