namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRoleTable : DbMigration
    {
        public override void Up()
        {
            //chức năng về tài khoản
            Sql("Insert into Roles (ID,Name) values (N'VIEW_USER',N'Xem danh sách người dùng')");
            Sql("Insert into Roles (ID,Name) values (N'ADD_USER',N'Thêm người dùng')");
            Sql("Insert into Roles (ID,Name) values (N'EDIT_USER',N'Sửa thông tin người dùng')");
            Sql("Insert into Roles (ID,Name) values (N'DELETE_USER',N'Xóa người dùng')");

            //chức năng 
        }
        
        public override void Down()
        {
        }
    }
}
