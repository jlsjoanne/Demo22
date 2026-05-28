namespace Demo22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberPwdPerm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "PasswordSalt", c => c.String(maxLength: 100));
            AddColumn("dbo.Members", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Members", "Permission", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Permission");
            DropColumn("dbo.Members", "Password");
            DropColumn("dbo.Members", "PasswordSalt");
        }
    }
}
