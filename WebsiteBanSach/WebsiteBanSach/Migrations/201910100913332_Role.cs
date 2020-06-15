namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role : DbMigration
    {
        public override void Up()
        {
            //Role
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_AUTHOR', N'Thêm tác giả mới')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_BOOK', N'Thêm sách mới')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_CATEGORY', N'Thêm thể loại mới')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_ORDER', N'Thêm đơn hàng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_PERMISSIONS', N'Thêm quyền cho nhóm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_PUBLISHER', N'Thêm nhà xuất bản')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_USER', N'Thêm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'ADD_USERGROUP', N'Thêm nhóm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELET_CATEGORY', N'Xoá thể loại')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_AUTHOR', N'Xoá tác giả')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_BOOK', N'Xóa sách')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_ORDER', N'Xóa đơn hàng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_PERMISSIONS', N'Xóa quyền thuộc nhóm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_PUBLISHER', N'Xoá nhà xuất bản')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_USER', N'Xóa người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'DELETE_USERGROUP', N'Xóa nhóm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_AUTHOR', N'Sửa thông tin tác giả')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_BOOK', N'Sửa thông tin sách')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_CATEGORY', N'Sửa thông tin thể loại')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_ORDER', N'Sửa thông tin đơn hàng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_PUBLISHER', N'Sửa thông tin nhà xuất bản')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_USER', N'Sửa thông tin người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_USER_SELF', N'Sửa thông tin tài khoản của bản thân')");
            Sql("Insert into Roles (ID, Name) VALUES(N'EDIT_USERGROUP', N'Sửa nhóm người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'TEST', N'test')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_AUTHOR', N'Xem thông tin tác giả')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_BOOK', N'Xem thông tin sách')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_CATEGORY', N'Xem thông tin thể loại')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_ORDER', N'Xem thông tin đơn hàng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_PUBLISHER', N'Xem thông tin nhà xuất bản')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_ROLE', N'Xem danh sách quyền')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_USER', N'Xem danh sách người dùng')");
            Sql("Insert into Roles (ID, Name) VALUES(N'VIEW_USERGROUP', N'Xem thông tin nhóm người dùng và quyền của nhóm')");
            //Role end
        }

        public override void Down()
        {
        }
    }
}
