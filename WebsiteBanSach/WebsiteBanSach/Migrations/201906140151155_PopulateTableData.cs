namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTableData : DbMigration
    {
        public override void Up()
        {
            
            //Categories
            Sql("SET IDENTITY_INSERT Categories ON;" +
                "Insert into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values(1,N'Tiểu thuyết',N'tieu-thuyet',N'','2019-04-21',N'Admin','2019-04-21',N'Admin');" +
                "INSERT into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(2,N'Truyện tranh', N'truyen-tranh', N'Tiểu thuyết là một thể loại văn xuôi có hư cấu, thông qua nhân vật, hoàn cảnh, sự việc để phản ánh bức tranh xã hội rộng lớn và những vấn đề của cuộc sống con người, biểu hiện tính chất tường thuật, tính chất kể chuyện bằng ngôn ngữ văn xuôi theo những chủ đề xác định.', CAST(N'2019-04-25T21:31:16.307' AS DateTime), N'Admin', CAST(N'2019-04-25T21:31:16.307' AS DateTime), N'Admin')"+
                "Insert into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(3,N'Tiểu thuyết', N'tieu-thuyet', N'<p>Tiểu thuyết l&agrave; một thể loại văn xu&ocirc;i c&oacute; hư cấu, th&ocirc;ng qua nh&acirc;n vật, ho&agrave;n cảnh, sự việc để phản &aacute;nh bức tranh x&atilde; hội rộng lớn v&agrave; những vấn đề của cuộc sống con người, biểu hiện t&iacute;nh chất tường thuật, t&iacute;nh chất kể chuyện bằng ng&ocirc;n ngữ văn xu&ocirc;i theo những chủ đề x&aacute;c định.</p>', CAST(N'2019-04-25T22:29:27.130' AS DateTime), N'Admin', CAST(N'2019-04-25T22:29:27.130' AS DateTime), N'Admin')"+
                "Insert into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(4,N'Trinh thám', N'trinh-tham', N'<p>mmmm</p>', CAST(N'2019-04-25T22:29:58.340' AS DateTime), N'Admin', CAST(N'2019-06-12T10:32:56.570' AS DateTime), N'Admin')"+
                "Insert into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(5,N'Giáo dục', N'giao-duc', N'<p>ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff</p>', CAST(N'2019-04-25T23:07:53.580' AS DateTime), N'Admin', CAST(N'2019-04-25T23:12:44.100' AS DateTime), N'Admin')"+
                "Insert into Categories (ID,Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(6,N'Kinh dị', N'kinh-di', N'<p>nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn</p>', CAST(N'2019-05-01T00:12:55.880' AS DateTime), N'Admin', CAST(N'2019-05-01T00:12:55.880' AS DateTime), N'Admin')"+
                "SET IDENTITY_INSERT Categories OFF;");
            //Categories end */
            
        }

        public override void Down()
        {
        }
    }
}
