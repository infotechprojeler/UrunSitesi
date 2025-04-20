using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrunSitesi.Core.Entities;
using UrunSitesi.Service;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _contactService;

        public ContactsController(IService<Contact> contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactService.GetAllAsync();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {            
            var model = _contactService.Find(id);
            if (model is null)
            {
                return NotFound("Id ile Eşleşen Kayıt Bulunamadı!");
            }
            return model;
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task PostAsync([FromBody] Contact value)
        {
            await _contactService.AddAsync(value);
            await _contactService.SaveAsync();
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Contact value)
        {
            _contactService.Update(value);
            _contactService.Save();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var kayit = _contactService.Find(id);
            if (kayit != null)
            {
                return Ok(kayit);
            }
            return NotFound();
        }
    }
}
