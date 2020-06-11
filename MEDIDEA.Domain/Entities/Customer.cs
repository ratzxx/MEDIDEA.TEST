using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEDIDEA.Domain.Entities
{
    public enum Gender
    {
        Male,
        Female,
        TransgenderFemale,
        TransgenderMale
    }
    public class Customer : FullAuditedEntity<long>
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
