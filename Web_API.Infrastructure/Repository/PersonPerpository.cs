using Web_API.Infrastructure.Data;
using Web_API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Infrastructure.Repository
{
    public class PersonPerpository
    {
        private readonly Context _context;

        public Context Context { get { return _context; } }

        public PersonPerpository(Context context)
        {
            _context = context;
        }
        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.Persons
                .Include(p => p.Phones)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
#pragma warning disable CS8603
        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .Where(p => p.Id == id)
                .Include(p => p.Phones)
                .FirstOrDefaultAsync();
        }
        public async Task<Person> GetByName(string name)
        {
            return await _context.Persons
                .Where(p => p.Name == name)
                .Include(p => p.Phones)
                .FirstOrDefaultAsync();
        }
#pragma warning restore CS8603
        public async Task<Person> AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
#pragma warning disable
        public async Task UpdateAsync(Person person)
        {
            var personFromDb = GetByIdAsync(person.Id).Result; 
            if (personFromDb is not null)
            {
                _context.Entry(personFromDb).CurrentValues.SetValues(person);    
                foreach (var phoneNumber in person.Phones)
                {
                    var phoneNumberFromDb = _context.Phones
                        .FirstOrDefault(phone => phone.Id == phoneNumber.Id);
                    if (phoneNumberFromDb is not null)
                        personFromDb.Phones.Add(phoneNumber);
                    else
                        _context.Entry(phoneNumberFromDb).CurrentValues.SetValues(phoneNumber);
                }
                foreach (var phoneNumberFromDb in personFromDb.Phones)
                {
                    if (!person.Phones.Any(phone => phone.Id == phoneNumberFromDb.Id))
                        personFromDb.Phones.Remove(phoneNumberFromDb);
                }
            }
            await _context.SaveChangesAsync();
        }
#pragma warning restore
        public async Task DeleteByIdAsync(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);  
            if (person is not null)
            {
                _context.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
