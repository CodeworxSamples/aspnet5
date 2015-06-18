using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using VNextDemo.Model;

namespace VNextDemo.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private BookContext context;

        public BookController(BookContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 0)]
        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await this.context.Books.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        [ResponseCache(Duration = 0)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await this.context.Books.FirstOrDefaultAsync(p => p.Id == id);
            if (result != null)
                return new ObjectResult(result);
            else
                return HttpNotFound();
        }

        [HttpGet("{title:regex(\\w)}")]
        [ResponseCache(Duration = 0)]
        public async Task<IActionResult> GetByTitleAsync(string title)
        {
            var result = await this.context.Books.FirstOrDefaultAsync(p => p.Title == title);

            if (result != null)
                return new ObjectResult(result);
            else
                return HttpNotFound();
        }

        [HttpPost()]
        public async Task<Book> AddAsync([FromBody] Book book)
        {
            this.context.Books.Add(book);
            await this.context.SaveChangesAsync();
            return book;
        }

        [HttpPut()]
        public async Task<Book> UpdateAsync([FromBody] Book book)
        {
            this.context.Entry(book).State = Microsoft.Data.Entity.EntityState.Modified;
            await this.context.SaveChangesAsync();
            return book;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await this.context.Books.FirstOrDefaultAsync(p => p.Id == id);
            if (item != null) {
                this.context.Books.Remove(item);
                await this.context.SaveChangesAsync();
                return new ObjectResult(item);
            }
            return HttpNotFound();
        }
    }
}
