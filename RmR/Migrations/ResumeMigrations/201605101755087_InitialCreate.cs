namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        ResumeName = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        Client_ID = c.Int(),
                        Expert_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ResumeID)
                .ForeignKey("dbo.Users", t => t.Client_ID)
                .ForeignKey("dbo.Users", t => t.Expert_ID)
                .Index(t => t.Client_ID)
                .Index(t => t.Expert_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resumes", "Expert_ID", "dbo.Users");
            DropForeignKey("dbo.Resumes", "Client_ID", "dbo.Users");
            DropIndex("dbo.Resumes", new[] { "Expert_ID" });
            DropIndex("dbo.Resumes", new[] { "Client_ID" });
            DropTable("dbo.Resumes");
            DropTable("dbo.Users");
        }
    }
}
