namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface ICreationAudited 
    {
        long? CreatorUserId { get; set; }
    }
}