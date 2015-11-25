namespace ITI.Archi_Vite.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Pseudo", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Followers", "FilePath");
            DropColumn("dbo.Patients", "PathFiles");
            DropColumn("dbo.Users", "Email");       
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Patients", "PathFiles", c => c.String(nullable: false));
            AddColumn("dbo.Followers", "FilePath", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Pseudo");
        }
    }
}
