namespace DevOps2017.EfForDotNetFramework.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonPhone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.Person", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phone", "PersonId", "dbo.Person");
            DropIndex("dbo.Phone", new[] { "PersonId" });
            DropColumn("dbo.Person", "Status");
            DropTable("dbo.Phone");
        }
    }
}
