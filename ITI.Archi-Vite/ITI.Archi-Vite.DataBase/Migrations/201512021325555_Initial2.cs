namespace ITI.Archi_Vite.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "Referent_ProfessionalId", "dbo.Professionals");
            DropIndex("dbo.Patients", new[] { "Referent_ProfessionalId" });
            DropColumn("dbo.Patients", "Referent_ProfessionalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Referent_ProfessionalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "Referent_ProfessionalId");
            AddForeignKey("dbo.Patients", "Referent_ProfessionalId", "dbo.Professionals", "ProfessionalId", cascadeDelete: true);
        }
    }
}
