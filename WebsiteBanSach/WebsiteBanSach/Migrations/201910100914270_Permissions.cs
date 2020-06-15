namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permissions : DbMigration
    {
        public override void Up()
        {
            //Permissions
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'ADD_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'ADD_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'ADD_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'ADD_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_PERMISSIONS')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'ADD_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'ADD_USERGROUP')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELET_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_PERMISSIONS')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'DELETE_USERGROUP')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'EDIT_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'EDIT_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'EDIT_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'EDIT_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_USER_SELF')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'EDIT_USER_SELF')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'EDIT_USER_SELF')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'StandbyAccount', N'EDIT_USER_SELF')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'EDIT_USERGROUP')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_AUTHOR')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_BOOK')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_CATEGORY')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_ORDER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_PUBLISHER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_ROLE')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_ROLE')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Member', N'VIEW_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_USER')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Admin', N'VIEW_USERGROUP')");
            Sql("Insert into Permissions (UserGroupID, RoleID) VALUES(N'Mod', N'VIEW_USERGROUP')");
            //Permissions end
        }

        public override void Down()
        {
        }
    }
}
