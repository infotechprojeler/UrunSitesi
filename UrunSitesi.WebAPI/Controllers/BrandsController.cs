using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public BrandsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<BrandsController>
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return _dbContext.Brands;
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            return _dbContext.Brands.Find(id);
        }

        // POST api/<BrandsController>
        [HttpPost]
        public void Post([FromBody] Brand value)
        {
            _dbContext.Brands.Add(value);
            _dbContext.SaveChanges();
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Brand value)
        {
            _dbContext.Brands.Update(value);
            _dbContext.SaveChanges();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var kayit = _dbContext.Brands.Find(id);
            if (kayit != null)
            {
                _dbContext.Brands.Remove(kayit);
                _dbContext.SaveChanges();
            }
        }
    }
}
