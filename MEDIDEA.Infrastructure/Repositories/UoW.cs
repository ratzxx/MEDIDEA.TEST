using MEDIDEA.Domain;
using MEDIDEA.Domain.Entities;

namespace MEDIDEA.Infrastructure.Repositories
{
    public class UoW : IUoW
    {
        private MedideaContext _context;
        private IRepository<Customer> _customers;
        private IVisitRepository _visits;
        private IRepository<Phone> _phones;

        public IRepository<Customer> Customers => _customers ?? (_customers = new CustomerRepository(_context));

        public IVisitRepository Visits => _visits ?? (_visits = new VisitRepository(_context));

        public IRepository<Phone> Phones => _phones ?? (_phones = new PhoneRepository(_context));

        public UoW(MedideaContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}