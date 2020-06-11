using System;

namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>Deletion time of this entity.</summary>
        DateTime? DeletionTime { get; set; }
    }
}