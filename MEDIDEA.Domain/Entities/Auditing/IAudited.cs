namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IAudited : ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime
    {
    }
}