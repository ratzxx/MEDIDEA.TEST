﻿namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IModificationAudited : IHasModificationTime
    {
        long? LastModifierUserId { get; set; }
    }
}