using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SecondTask.Models
{
    public class DataPersonRepository : IPersonRepository
    {
        private readonly DataContext context;

        public DataPersonRepository(DataContext context)
        {
            this.context = context;
        }
        public Task<Person> FindByAccountIdAsync(string accountId)
        {
            return context.Persons.Where(x => x.AccountId == accountId)
            .SingleOrDefaultAsync();
        }

        public async Task AddAsync(Person person)
        {
            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await context.Persons.ToListAsync();
        }

        public async Task DeleteUserAsync(string[] accountId)
        {
            foreach(var item in accountId)
            {
                var personForDelete = await FindByAccountIdAsync(item);
                context.Remove(personForDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task BlockUserAsync(string[] accountId)
        {
           foreach(var item in accountId)
            {
                var personForBlock = await FindByAccountIdAsync(item);
                personForBlock.IsBlocked = personForBlock.IsBlocked == true ? false : true;
            }
            await context.SaveChangesAsync();
        }

        public async Task LastInUpdate(string accountId)
        {
            var personForUpdate = await FindByAccountIdAsync(accountId);
            personForUpdate.LastIn = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }
}
