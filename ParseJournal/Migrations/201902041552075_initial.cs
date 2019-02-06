namespace ParseJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ParsedDate = c.DateTime(nullable: false),
                        stringDate = c.String(),
                        text = c.String(),
                        length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Entries");
        }
    }
}
