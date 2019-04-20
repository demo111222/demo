namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserGroupTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into UserGroups (ID,Name) values (N'Admin',N'Quản trị viên')");
            Sql("Insert into UserGroups (ID,Name) values (N'Member',N'Người dùng')");
            Sql("Insert into UserGroups (ID,Name) values (N'Mod',N'Nhân viên')");
        }
        
        public override void Down()
        {
        }
    }
}
