using MEDIDEA.Domain;
using MEDIDEA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDIDEA.Infrastructure.Repositories
{
    public class VisitRepository: GenericRepository<Visit>, IVisitRepository
    {
        public VisitRepository(MedideaContext context) : base(context)
        {
        }
    }
}
