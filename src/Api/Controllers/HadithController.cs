using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasaneef.Models;

namespace Tasaneef.Controllers
{
    [ApiController]
    [Route("")]
    public class HadithController : ControllerBase
    {
        private readonly IDBContext _dbContext;

        private static readonly string[] Books = {  "abodawud"
                                                    ,"aldarimi"
                                                    ,"alnasai"
                                                    ,"altirmidhi"
                                                    ,"bukhari"
                                                    ,"ibnhanbal"
                                                    ,"ibnmaja"
                                                    ,"muslim"
                                                    ,"muwataa"};

        public HadithController(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("{book}/{num}")]
        [ProducesResponseType(typeof(Hadith), StatusCodes.Status200OK)]
        public async Task<ActionResult<Hadith>> Get(string book, int num)
        {
            book = book?.ToString().Trim();

            if (string.IsNullOrEmpty(book) || num <= 0) return base.BadRequest();
            if (!Books.Contains(book.ToLower())) return NotFound("Invalid book");

            var hadith = _dbContext.Hadiths.FirstOrDefault(x => x.Book == book.ToLower() && x.Number == num);

            await Task.CompletedTask;

            return Ok(hadith);
        }

        [Route("{book}/{start}/{size}")]
        [ProducesResponseType(typeof(Hadith), StatusCodes.Status200OK)]
        public async Task<ActionResult<Hadith>> List(string book, int start, int size)
        {
            book = book?.ToString().Trim();
            if (string.IsNullOrEmpty(book)) return BadRequest();

            if (!Books.Contains(book.ToLower())) return NotFound("Invalid book");

            var maxSize = 50;

            start = start <= 0 ? 1 : start;
            size = (size <= 0 || size > maxSize) ? maxSize : size;

            var hadiths = _dbContext.Hadiths
                .Where(x => x.Book == book.ToLower())
                .OrderBy(x => x.Number)
                .Skip(start - 1)
                .Take(size)
                .ToList();

            if (hadiths.Count() == 0) return NotFound();

            await Task.CompletedTask;

            return Ok(hadiths);
        }
    }
}