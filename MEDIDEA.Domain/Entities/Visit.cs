using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDIDEA.Domain.Entities
{
    public enum VisitType:byte
    {
        First,
        Second,
        Condition
    }

    public class Visit: FullAuditedEntity<long>
    {
        [Required]
        public VisitType Type { get; set; }

        public string Description { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
