namespace Atm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        LoginAttemptCount = c.Int(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AtmCards",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Number = c.String(),
                        Pin = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OperationJournals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        OperationCode = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AtmCard_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AtmCards", t => t.AtmCard_Id)
                .Index(t => t.AtmCard_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationJournals", "AtmCard_Id", "dbo.AtmCards");
            DropForeignKey("dbo.AtmCards", "Id", "dbo.Users");
            DropForeignKey("dbo.Accounts", "User_Id", "dbo.Users");
            DropIndex("dbo.OperationJournals", new[] { "AtmCard_Id" });
            DropIndex("dbo.AtmCards", new[] { "Id" });
            DropIndex("dbo.Accounts", new[] { "User_Id" });
            DropTable("dbo.OperationJournals");
            DropTable("dbo.AtmCards");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
