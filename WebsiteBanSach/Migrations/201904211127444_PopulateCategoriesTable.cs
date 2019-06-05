namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Categories (Name,MetaTitle,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values (N'Tiểu thuyết',N'tieu-thuyet',N'Tiểu thuyết là một thể loại văn xuôi có hư cấu, thông qua nhân vật, hoàn cảnh, sự việc để phản ánh bức tranh xã hội rộng lớn và những vấn đề của cuộc sống con người, biểu hiện tính chất tường thuật, tính chất kể chuyện bằng ngôn ngữ văn xuôi theo những chủ đề xác định.','2019-04-21',N'Admin','2019-04-21',N'Admin')");
        }
        
        public override void Down()
        {
        }
    }
}
