using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondTask.Models
{
 public interface IPersonRepository {

        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person> FindByAccountIdAsync(string accountId);
        Task AddAsync(Person person);

        Task DeleteUserAsync(string[] accountId);

        Task BlockUserAsync(string[] accountId);

        Task LastInUpdate(string accountId);
    }
}