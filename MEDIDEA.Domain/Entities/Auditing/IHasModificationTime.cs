using System;

namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}