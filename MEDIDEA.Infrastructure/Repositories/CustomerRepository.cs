using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MEDIDEA.Domain;
using MEDIDEA.Domain.Entities;

namespace MEDIDEA.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(MedideaContext context):base(context)
        {
        }

        public async Task<IEnumerable<Phone>> GetPhones(long customerId)
        {
            var phones = await _context.Phones
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();
            return phones;
        }

        public Task<int> AddPhone(Phone phone)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePhone(long phoneId)
        {
            throw new System.NotImplementedException();
        }
    }
}
