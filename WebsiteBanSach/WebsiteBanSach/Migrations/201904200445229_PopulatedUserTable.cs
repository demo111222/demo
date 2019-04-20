namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatedUserTable : DbMigration
    {
        public override void Up()
        {
            //Nếu trước giờ chưa tải về chạy bao giờ thì không cần chạy cái AddColumn
            //AddColumn("dbo.Users", "Gender", c => c.Boolean(nullable: true));
            Sql("Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values (N'Admin',N'123',N'Admin',N'Nguyễn Văn A',0,N'user.png',N'123 Điện Biên Phủ, Quận Bình Thạnh ,TP.HCM',N'09XXXXXX',N'nva@gmail.com','04-20-2019',N'Admin','04-20-2019',N'Admin')");
            Sql("Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values (N'Khachhang',N'123',N'Member',N'Trần Thị B',0,N'user.png',N'456 Điện Biên Phủ',N'09XXXXXX',N'tvb@gmail.com','04-20-2019',N'Khachhang','04-20-2019',N'Khachhang')");
        }

        public override void Down()
        {
            //DropColumn("dbo.Users", "Gender");
        }
    }
}
