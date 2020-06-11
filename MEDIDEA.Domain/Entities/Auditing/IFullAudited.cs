namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IFullAudited : IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete
    {
    }
}