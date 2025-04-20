using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public SlidersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Slider> Get()
        {
            return _dbContext.Sliders;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Slider Get(int id)
        {
            return _dbContext.Sliders.Find(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task PostAsync([FromBody] Slider value)
        {
            await _dbContext.Sliders.AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Slider value)
        {
            _dbContext.Sliders.Update(value);
            await _dbContext.SaveChangesAsync();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            _dbContext.Sliders.Remove(Get(id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
