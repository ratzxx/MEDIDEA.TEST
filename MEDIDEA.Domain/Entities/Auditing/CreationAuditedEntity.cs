using System;
using MEDIDEA.Core.Timing;

namespace MEDIDEA.Domain.Entities.Auditing
{
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited, IHasCreationTime
    {
        public virtual DateTime CreationTime { get; set; }

        public virtual long? CreatorUserId { get; set; }

        protected CreationAuditedEntity()
        {
            this.CreationTime = Clock.Now;
        }
    }
}