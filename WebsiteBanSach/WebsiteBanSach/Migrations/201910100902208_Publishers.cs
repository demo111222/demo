namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publishers : DbMigration
    {
        public override void Up()
        {
            /*
            //Publishers
            Sql("SET IDENTITY_INSERT Publishers ON;"
                + "Insert into Publishers (ID,Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(1,N'Nhà xuất bản trẻ', N'nha-xuat-ban-tre', N'NXB-Tre.jpg', CAST(N'2019-04-26T10:58:39.460' AS DateTime), N'Admin', CAST(N'2019-04-26T12:48:04.243' AS DateTime), N'Admin')"
                + "Insert into Publishers (ID,Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(2,N'Nhà xuất bản già', N'nha-xuat-ban-gia', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-15T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Publishers (ID,Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(3,N'Nhà xuất bản IPM', N'nha-xuat-ban-ipm', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-16T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Publishers (ID,Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(4,N'Nhà xuất bản giáo dục', N'nha-xuat-ban-giao-duc', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-17T00:00:00.000' AS DateTime), N'Admin')"
                + "SET IDENTITY_INSERT Publishers OFF;");

            //Publishers end */
            Sql( "Insert into Publishers (Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nhà xuất bản trẻ', N'nha-xuat-ban-tre', N'NXB-Tre.jpg', CAST(N'2019-04-26T10:58:39.460' AS DateTime), N'Admin', CAST(N'2019-04-26T12:48:04.243' AS DateTime), N'Admin')"
                + "Insert into Publishers (Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nhà xuất bản già', N'nha-xuat-ban-gia', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-15T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Publishers (Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nhà xuất bản IPM', N'nha-xuat-ban-ipm', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-16T00:00:00.000' AS DateTime), N'Admin')"
                + "Insert into Publishers (Name,MetaTitle,Logo,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Nhà xuất bản giáo dục', N'nha-xuat-ban-giao-duc', N'NXB-Tre.jpg', CAST(N'2019-05-01T00:00:00.000' AS DateTime), N'Admin', CAST(N'2019-05-17T00:00:00.000' AS DateTime), N'Admin')"
                );
        }

        public override void Down()
        {
        }
    }
}
