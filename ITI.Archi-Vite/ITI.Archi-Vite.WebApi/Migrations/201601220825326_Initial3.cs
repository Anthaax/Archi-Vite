namespace ITI.Archi_Vite.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        PatientId = c.Int(nullable: false),
                        ProfessionnalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatientId, t.ProfessionnalId })
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Professionals", t => t.ProfessionnalId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.ProfessionnalId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.Users", t => t.PatientId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Adress = c.String(nullable: false, storeType: "ntext"),
                        City = c.String(nullable: false),
                        Postcode = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Pseudo = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Photo = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Professionals",
                c => new
                    {
                        ProfessionalId = c.Int(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProfessionalId)
                .ForeignKey("dbo.Users", t => t.ProfessionalId)
                .Index(t => t.ProfessionalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followers", "ProfessionnalId", "dbo.Professionals");
            DropForeignKey("dbo.Professionals", "ProfessionalId", "dbo.Users");
            DropForeignKey("dbo.Followers", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "PatientId", "dbo.Users");
            DropIndex("dbo.Professionals", new[] { "ProfessionalId" });
            DropIndex("dbo.Patients", new[] { "PatientId" });
            DropIndex("dbo.Followers", new[] { "ProfessionnalId" });
            DropIndex("dbo.Followers", new[] { "PatientId" });
            DropTable("dbo.Professionals");
            DropTable("dbo.Users");
            DropTable("dbo.Patients");
            DropTable("dbo.Followers");
        }
    }
}
