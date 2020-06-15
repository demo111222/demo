namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            //Users
            Sql(
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Admin'   , N'123'        , N'Admin'         , N'Nguyễn Văn A'  , 0, N'user.png', N'123 Điên Biên Phủ, Quận Bình Thạnh ,TP.HCM'                               , N'09XXXXXX'  , N'nva@gmail.com'            , CAST(N'2019-04-20T00:00:00.000' AS DateTime), N'Admin'   , CAST(N'2019-04-20T00:00:00.000' AS DateTime), N'Admin')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'faker'   , N'faker1'     , N'Member'        , N'Phan Nhật Minh', 0, N'user.png', N'C17/34 ấp 3 đường Đinh Đức Thiện, xã Bình Chánh, Huyện Bình Chánh, TP.HCM', N'0903098078', N'gate.braille@gmail.com'   , CAST(N'2019-04-20T21:15:55.227' AS DateTime), N'faker'   , CAST(N'2019-06-11T23:59:44.327' AS DateTime), N'faker')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Mod'     , N'456'        , N'Mod'           , N'Phan Văn A'    , 0, N'user.png', N'122 TP.Hồ Chí Minh'                                                       , N'09XXXXXX'  , N'mail@gmail.com'           , CAST(N'2019-04-25T00:00:00.000' AS DateTime), N'Mod'     , CAST(N'2019-04-25T00:00:00.000' AS DateTime), N'Mod')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'Chicken' , N'vpw3iaq3lrh', N'StandbyAccount', N'Minh test'     , 0, N'user.png', N'12mmm'                                                                    , N'1929929xx' , N'farenjam@gmail.com'       , CAST(N'2019-04-12T00:00:00.000' AS DateTime), N'Admin'   , CAST(N'2019-05-31T19:16:44.017' AS DateTime), N'Admin')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'ngoquyen', N'1234567'    , N'Member'        , N'Nguyễn Văn B'  , 0, N'user.png', N'23 Điện Biên Phủ, Quận Bình Thạnh, Hồ Chí Minh city'                      , N'0900200033', N'maymay12322@gmail.com'    , CAST(N'2019-06-10T11:30:37.813' AS DateTime), N'ngoquyen', CAST(N'2019-06-10T11:30:37.813' AS DateTime), N'ngoquyen')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'lienquan', N'zstt9923ZSz', N'Member'        , N'Nguyễn Văn C'  , 0, N'user.png', N'24 dien bien phu, quan binh thanh, ho chi minh'                           , N'0903098000', N'binhminh1213142@gmail.com', CAST(N'2019-06-10T11:44:58.743' AS DateTime), N'lienquan', CAST(N'2019-06-13T23:21:01.357' AS DateTime), N'Hệ thống')" +
                "Insert into Users (UserName,Password,UserGroupID,Name,Gender,Avatar,Address,Phone,Email,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) VALUES(N'KOI'     , N'LAB'        , N'Member'        , N'Phan V A'      , 0, N'user.png', N'C17/34 ấp 3 đường Đinh Đức Thiện, xã Bình Chánh, Huyện Bình Chánh, TP.HCM', N'903098078' , N'binhminh1213142@gmail.com', CAST(N'2019-06-13T23:34:34.953' AS DateTime), N'KOI'     , CAST(N'2019-06-13T23:34:34.953' AS DateTime), N'KOI')");
            //Users end
        }

        public override void Down()
        {
        }
    }
}
