namespace Wpf10_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblUsers10_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUsers10_1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblUsers10_1");
        }
    }
}
