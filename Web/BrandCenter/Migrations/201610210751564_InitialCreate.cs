namespace BrandCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Desc = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);

            RenameTable("Group", "tblGroup");

        }

        public override void Down()
        {
            DropTable("dbo.Group");
            RenameTable("tblGroup", "Group");
        }

    }

}
