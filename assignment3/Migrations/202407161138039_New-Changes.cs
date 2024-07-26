namespace assignment3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblbook",
                c => new
                    {
                        Bookid = c.Int(nullable: false, identity: true),
                        Bookname = c.String(nullable: false, maxLength: 50),
                        Publishername = c.String(nullable: false, maxLength: 50),
                        Bookedition = c.String(nullable: false, maxLength: 50),
                        ContentType = c.String(),
                        Image = c.Binary(),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Bookid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblbook");
        }
    }
}
