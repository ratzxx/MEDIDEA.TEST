using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MEDIDEA.Core.Timing;
using MEDIDEA.Domain.Entities;
using MEDIDEA.Domain.Entities.Auditing;

namespace MEDIDEA.Infrastructure
{
    public class MedideaContext : DbContext
    {
        public MedideaContext() : base("MedideaDb")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added || i.State == EntityState.Modified)
                .ToList();
            foreach (var entity in entities)
            {
                CreationAuditedActions(entity);
                DeletionAuditedActions(entity);
                ModificationAuditedActions(entity);
            }
            return base.SaveChanges();
        }

        private static void CreationAuditedActions(DbEntityEntry entity)
        {
            if (entity is IHasCreationTime e && entity.State == EntityState.Added)
            {
                e.CreationTime = Clock.Now;
            }
        }
        private static void DeletionAuditedActions(DbEntityEntry entity)
        {
            if (entity is IDeletionAudited e && e.IsDeleted)
            {
                e.DeletionTime = Clock.Now;
                // e.DeleterUserId = ...
            }
        }
        private static void ModificationAuditedActions(DbEntityEntry entity)
        {
            if (entity is IModificationAudited e && entity.State == EntityState.Modified)
            {
                e.LastModificationTime = Clock.Now;
                // e.LastModifierUserId = ...
            }
        }
    }
}
