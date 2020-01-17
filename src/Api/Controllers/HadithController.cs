using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasaneef.Models;

namespace Tasaneef.Controllers
{
    [ApiController]
    [Route("")]
    public class HadithController : ControllerBase
    {
        private readonly IDBContext _dbContext;
        private readonly IRandom _random;

        private static readonly Dictionary<string, int> HadithCount = new Dictionary<string, int>{
                                                     {"abodawud", 4590 }
                                                    ,{"aldarimi",3367 }
                                                    ,{"alnasai",5662 }
                                                    ,{"altirmidhi",3891 }
                                                    ,{"bukhari",7008 }
                                                    ,{"ibnhanbal",26363 }
                                                    ,{"ibnmaja",4332 }
                                                    ,{"muslim",5362 }
                                                    ,{"muwataa",1594}};

        public HadithController(IDBContext dbContext, IRandom random)
        {
            _dbContext = dbContext;
            _random = random;
        }

        [Route("{book}/{num}")]
        [ProducesResponseType(typeof(HadithDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HadithDto>> Get(string book, int num)
        {
            book = book?.ToString().Trim().ToLower();

            if (string.IsNullOrEmpty(book) || num <= 0) return BadRequest();
            if (!HadithCount.TryGetValue(book, out var count)) return NotFound("Invalid book");
            if (num > count) return NotFound();

            var hadith = await _dbContext
                .Hadiths
                .FirstOrDefaultAsync(x => x.Book == book && x.Number == num);

            if (hadith == null) return NotFound();

            return Ok(new HadithDto(hadith));
        }

        [Route("{book}/{start}/{size}")]
        [ProducesResponseType(typeof(IEnumerable<HadithDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HadithDto>>> List(string book, int start, int size)
        {
            book = book?.ToString().Trim();
            if (string.IsNullOrEmpty(book)) return BadRequest();

            if (!HadithCount.TryGetValue(book, out var count)) return NotFound("Invalid book");
            if (start > count) return NotFound();

            var maxSize = 50;

            start = start <= 0 ? 1 : start;
            size = (size <= 0 || size > maxSize) ? maxSize : size;

            var hadiths = await _dbContext.Hadiths
                .Where(x => x.Book == book)
                .OrderBy(x => x.Number)
                .Skip(start - 1)
                .Take(size)
                .ToListAsync();

            return Ok(hadiths.Select(x => new HadithDto(x)));
        }

        [Route("random/{book?}")]
        [ProducesResponseType(typeof(HadithDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HadithDto>> Random(string book = "bukhari")
        {
            book = book.Trim().ToLower();

            if (!HadithCount.TryGetValue(book, out var count)) return NotFound("Invalid book");

            var hadithNumber = _random.RandPositive(count);

            var hadith = await _dbContext
                .Hadiths
                .FirstOrDefaultAsync(x => x.Book == book && x.Number == hadithNumber);

            if (hadith == null) return NotFound();

            return Ok(new HadithDto(hadith));
        }
    }
}