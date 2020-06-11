using System;

namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}