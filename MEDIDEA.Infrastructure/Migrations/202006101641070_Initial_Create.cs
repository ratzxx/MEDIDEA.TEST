namespace MEDIDEA.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        Birthday = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 16),
                        Type = c.Byte(nullable: false),
                        CustomerId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Byte(nullable: false),
                        Description = c.String(),
                        CustomerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Phones", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Visits", new[] { "CustomerId" });
            DropIndex("dbo.Phones", new[] { "CustomerId" });
            DropTable("dbo.Visits");
            DropTable("dbo.Phones");
            DropTable("dbo.Customers");
        }
    }
}
