namespace Migration.SQLServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.migration_carmodel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.migration_car",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        CarModelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.migration_carmodel", t => t.CarModelId)
                .Index(t => t.CarModelId);
            
            CreateTable(
                "dbo.migration_cartrip",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        From = c.String(nullable: false, maxLength: 100),
                        To = c.String(nullable: false, maxLength: 100),
                        CarId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.migration_car", t => t.CarId)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.migration_cartrip", "CarId", "dbo.migration_car");
            DropForeignKey("dbo.migration_car", "CarModelId", "dbo.migration_carmodel");
            DropIndex("dbo.migration_cartrip", new[] { "CarId" });
            DropIndex("dbo.migration_car", new[] { "CarModelId" });
            DropTable("dbo.migration_cartrip");
            DropTable("dbo.migration_car");
            DropTable("dbo.migration_carmodel");
        }
    }
}
