namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserGroups : DbMigration
    {
        public override void Up()
        {
            //UserGroups
            Sql(
                "Insert into UserGroups (ID, Name) VALUES(N'Admin', N'Quản trị viên')" +
                "Insert into UserGroups (ID, Name) VALUES(N'Admintest', N'test mam')" +
                "Insert into UserGroups (ID, Name) VALUES(N'Member', N'Người dùng')" +
                "Insert into UserGroups (ID, Name) VALUES(N'Mod', N'Nhân viên')" +
                "Insert into UserGroups (ID, Name) VALUES(N'StandbyAccount', N'Đang chờ được phân nhóm')");
            //UserGroups end
        }

        public override void Down()
        {
        }
    }
}
