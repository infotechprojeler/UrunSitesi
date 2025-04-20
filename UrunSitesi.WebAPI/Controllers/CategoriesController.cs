using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _dbContext.Categories;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetAsync(int id)
        {
            var model = await _dbContext.Categories.FindAsync(id);
            if (model is not null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task Post([FromBody] Category value)
        {
            await _dbContext.Categories.AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category value)
        {
            _dbContext.Categories.Update(value);
            _dbContext.SaveChanges();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) // Task<ActionResult> döneceğimizi belirtirsek metot içerisinde return ok notfound badrequest vb durumlarını geriye dönebiliriz.
        {
            var kayit = await _dbContext.Categories.FindAsync(id);
            if (kayit is not null)
            {
                _dbContext.Categories.Remove(kayit);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
