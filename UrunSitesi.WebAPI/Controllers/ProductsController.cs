using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ProductsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var model = _dbContext.Products // veritabanından ürün listesini çek
                .Include(c => c.Category)  // ürünlere kategorilerini dahil et
                .Include(c => c.Brand);
            return model;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var model = _dbContext.Products
                .Where(c => c.Id == id)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .FirstOrDefault();
            if (model != null)
                return model;
            return NotFound("Kayıt Bulunamadı!");
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task PostAsync([FromBody] Product value)
        {
            await _dbContext.Products.AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            _dbContext.Products.Update(value);
            _dbContext.SaveChanges();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null) 
                return BadRequest();
            var model = await _dbContext.Products.FindAsync(id);
            if (model != null)
            {
                _dbContext.Products.Remove(model);
                await _dbContext.SaveChangesAsync();
                return Ok("Kayıt Silindi!");
            }
            return NotFound("Kayıt Bulunamadı!");
        }
    }
}
