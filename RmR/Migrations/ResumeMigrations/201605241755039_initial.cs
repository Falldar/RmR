namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 80),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        ResumeID = c.Int(nullable: false, identity: true),
                        ResumeName = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CompletedOn = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        ClientID = c.Int(nullable: false),
                        ExpertID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResumeID)
                .ForeignKey("dbo.Users", t => t.ClientID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ExpertID, cascadeDelete: false)
                .Index(t => t.ClientID)
                .Index(t => t.ExpertID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resumes", "ExpertID", "dbo.Users");
            DropForeignKey("dbo.Resumes", "ClientID", "dbo.Users");
            DropIndex("dbo.Resumes", new[] { "ExpertID" });
            DropIndex("dbo.Resumes", new[] { "ClientID" });
            DropTable("dbo.Resumes");
            DropTable("dbo.Users");
        }
    }
}
