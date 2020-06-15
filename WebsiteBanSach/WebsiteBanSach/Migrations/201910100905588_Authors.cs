namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authors : DbMigration
    {
        public override void Up()
        {
            //Authors
            Sql("Insert into Authors (Name,MetaTitle,Description,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Jon Snow', N'jon-snow', N'<p>Nh&agrave; văn chuy&ecirc;n viết s&aacute;ch gi&aacute;o khoa cho học sinh</p>', N'15.jpg', CAST(N'2019-04-27T10:12:49.623' AS DateTime), N'Admin', CAST(N'2019-05-02T00:11:27.287' AS DateTime), N'Admin')"
                + "Insert into Authors (Name,MetaTitle,Description,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nguyễn', N'nguyen', N'tac gia ngheo nhieu huy chuong', N'15.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-15T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Authors (Name,MetaTitle,Description,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Phan', N'phan', N'tac gia giau di len tu hai ban tay den', N'15.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-16T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Authors (Name,MetaTitle,Description,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Đinh', N'dinh', N'tac gia tre qua tre nhung viet chu cuc dep', N'15.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-17T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Authors (Name,MetaTitle,Description,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nguyễn Phú Trọng', N'nguyen-phu-trong', N'<p>T&aacute;c giả mới ...............................</p>', N'01.jpg', CAST(N'2019-05-31T15:24:18.587' AS DateTime), N'Admin', CAST(N'2019-05-31T15:24:18.587' AS DateTime), N'Admin')");
            //Authors end
        }

        public override void Down()
        {
        }
    }
}
