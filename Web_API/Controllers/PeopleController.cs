using Microsoft.AspNetCore.Mvc;
using Web_API.Domain.Models;
using Web_API.Infrastructure.Data;
using Web_API.Infrastructure.Repository;

namespace Web_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly Context _context;
        private readonly PersonPerpository _personRepo;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(Context context, ILogger<PeopleController> logger)
        {
            _context = context;
            _logger = logger;
            _personRepo = new(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _personRepo.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            return person is not null ? person : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
                return BadRequest();

            await _personRepo.UpdateAsync(person);
            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Person>> DeletPerson(Guid id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person is null)
                return NotFound();
            await _personRepo.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
