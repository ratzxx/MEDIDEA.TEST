namespace MEDIDEA.Domain.Entities.Auditing
{
    public interface IDeletionAudited : IHasDeletionTime, ISoftDelete
    {
        /// <summary>Which user deleted this entity?</summary>
        long? DeleterUserId { get; set; }
    }
}