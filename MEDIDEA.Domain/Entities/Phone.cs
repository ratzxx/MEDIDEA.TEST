using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEDIDEA.Domain.Entities.Auditing;

namespace MEDIDEA.Domain.Entities
{
    public enum PhoneType : byte
    {
        Mobile,
        Home,
        Business
    }
    public class Phone: CreationAuditedEntity<long>
    {
        [Required]
        [MaxLength(16)]
        public string Number { get; set; }

        [Required]
        public PhoneType Type { get; set; }

        public long? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
