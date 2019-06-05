using System.Web.Mvc;

namespace WebsiteBanSach.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_home",
                "Admin/trang-chu",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_home_danhmuc",
                "Admin/trang-chu/danh-muc",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_home_logout",
                "Admin/trang-chu/dang-xuat",
                new { controller = "Home", action = "LogOut", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );


            //Publisher
            context.MapRoute(
                "Admin_publishers",
                "Admin/trang-chu/danh-muc/nha-xuat-ban",
                new { controller = "Publishers", action = "Publishers", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_publishers_create",
                "Admin/trang-chu/danh-muc/nha-xuat-ban/them-moi",
                new { controller = "Publishers", action = "Create", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_publishers_detail",
                "Admin/trang-chu/danh-muc/nha-xuat-ban/chi-tiet-{id}/{MetaTitle}",
                new { controller = "Publishers", action = "Details", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_publishers_detail_pdf",
                "Admin/trang-chu/danh-muc/nha-xuat-ban/pdf-chi-tiet-{id}/{MetaTitle}",
                new { controller = "Publishers", action = "DetailsPDF", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_publishers_edit",
                "Admin/trang-chu/danh-muc/nha-xuat-ban/sua-{id}/{MetaTitle}",
                new { controller = "Publishers", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_publishers_delete",
                "Admin/trang-chu/danh-muc/nha-xuat-ban/xoa-{id}/{MetaTitle}",
                new { controller = "Publishers", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Publisher END

            //Author
            context.MapRoute(
                "Admin_Author",
                "Admin/trang-chu/danh-muc/tac-gia",
                new { controller = "Authors", action = "Authors", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Authors_create",
                "Admin/trang-chu/danh-muc/tac-gia/them-moi",
                new { controller = "Authors", action = "Create", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Authors_detail",
                "Admin/trang-chu/danh-muc/tac-gia/chi-tiet-{id}/{MetaTitle}",
                new { controller = "Authors", action = "Details", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Authors_detail_pdf",
                "Admin/trang-chu/danh-muc/tac-gia/pdf-chi-tiet-{id}/{MetaTitle}",
                new { controller = "Authors", action = "DetailsPDF", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Authors_edit",
                "Admin/trang-chu/danh-muc/tac-gia/sua-{id}/{MetaTitle}",
                new { controller = "Authors", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Authors_delete",
                "Admin/trang-chu/danh-muc/tac-gia/xoa-{id}/{MetaTitle}",
                new { controller = "Authors", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Author END

            //Categories
            context.MapRoute(
                "Admin_categories",
                "Admin/trang-chu/danh-muc/the-loai",
                new { controller = "Categories", action = "Categories", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_categories_create",
                "Admin/trang-chu/danh-muc/the-loai/them-moi",
                new { controller = "Categories", action = "Create", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_categories_detail",
                "Admin/trang-chu/danh-muc/the-loai/chi-tiet-{id}/{MetaTitle}",
                new { controller = "Categories", action = "Details", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_categories_detail_pdf",
                "Admin/trang-chu/danh-muc/the-loai/pdf-chi-tiet-{id}/{MetaTitle}",
                new { controller = "Categories", action = "DetailsPDF", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_categories_edit",
                "Admin/trang-chu/danh-muc/the-loai/sua-{id}/{MetaTitle}",
                new { controller = "Categories", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_categories_delete",
                "Admin/trang-chu/danh-muc/the-loai/xoa-{id}/{MetaTitle}",
                new { controller = "Categories", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Categories END

            //Books
            context.MapRoute(
                "Admin_books",
                "Admin/trang-chu/danh-muc/sach",
                new { controller = "Books", action = "Books", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_books_create",
                "Admin/trang-chu/danh-muc/sach/them-moi",
                new { controller = "Books", action = "Create", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_books_detail",
                "Admin/trang-chu/danh-muc/sach/chi-tiet-{id}/{MetaTitle}",
                new { controller = "Books", action = "Details", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_books_detail_pdf",
                "Admin/trang-chu/danh-muc/sach/pdf-chi-tiet-{id}/{MetaTitle}",
                new { controller = "Books", action = "DetailsPDF", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_books_edit",
                "Admin/trang-chu/danh-muc/sach/sua-{id}/{MetaTitle}",
                new { controller = "Books", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_books_delete",
                "Admin/trang-chu/danh-muc/sach/xoa-{id}/{MetaTitle}",
                new { controller = "Books", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Books END

            //Orders
            context.MapRoute(
                "Admin_orders_1",
                "Admin/trang-chu/don-dat-hang",
                new { controller = "Orders", action = "Orders", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_orders_2",
                "Admin/trang-chu/don-dat-hang/danh-sach",
                new { controller = "Orders", action = "Orders", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_orders_detail",
                "Admin/trang-chu/don-dat-hang/chi-tiet-don-dat-hang-{id}",
                new { controller = "Orders", action = "Details", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            
            context.MapRoute(
                "Admin_orders_edit",
                "Admin/trang-chu/don-dat-hang/sua-don-dat-hang-{id}",
                new { controller = "Orders", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_orders_delete",
                "Admin/trang-chu/don-dat-hang/xoa-don-dat-hang-{id}",
                new { controller = "Orders", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Orders END

            //Orders detail
            context.MapRoute(
                "Admin_orders_detail_edit",
                "Admin/trang-chu/don-dat-hang/sua-chi-tiet-don-dat-hang-{OrderID}/ma-sach-{id}",
                new { controller = "Orders", action = "EditDetail", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_orders_detail_delete",
                "Admin/trang-chu/don-dat-hang/xoa-chi-tiet-don-dat-hang-{OrderID}/ma-sach-{id}",
                new { controller = "Orders", action = "DeleteDetail", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Orders detail END

            //Users + RoleAndPermission
            context.MapRoute(
                "Admin_Users_1",
                "Admin/trang-chu/nguoi-dung",
                new { controller = "Users", action = "Users", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Users_1111",
                "Admin/trang-chu/nguoi-dung/danh-sach-cac-quyen",
                new { controller = "RoleAndPermission", action = "Role", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Users_22",
                "Admin/trang-chu/nguoi-dung/danh-sach-nguoi-dung-chua-co-nhom",
                new { controller = "Users", action = "StandbyAccount", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Users_2",
                "Admin/trang-chu/nguoi-dung/danh-sach-khach-hang",
                new { controller = "Users", action = "Users", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Users_3",
                "Admin/trang-chu/nguoi-dung/danh-sach-quan-tri-vien",
                new { controller = "Users", action = "Employee", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Users_createnew",
                "Admin/trang-chu/nguoi-dung/them-nguoi-dung",
                new { controller = "Users", action = "Create", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //group user
            context.MapRoute(
                    "Admin_Users_4",
                    "Admin/trang-chu/nguoi-dung/danh-sach-nhom-nguoi-dung",
                    new { controller = "RoleAndPermission", action = "UserGroups", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
            context.MapRoute(
                    "Admin_Users_4_create",
                    "Admin/trang-chu/nguoi-dung/them-nhom-nguoi-dung",
                    new { controller = "RoleAndPermission", action = "Create", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
            context.MapRoute(
                    "Admin_Users_4_edit",
                    "Admin/trang-chu/nguoi-dung/sua-nhom-nguoi-dung-{id}",
                    new { controller = "RoleAndPermission", action = "Edit", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
                context.MapRoute(
                    "Admin_Users_4_delete",
                    "Admin/trang-chu/nguoi-dung/xoa-nhom-nguoi-dung-{id}",
                    new { controller = "RoleAndPermission", action = "Delete", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
                context.MapRoute(
                    "Admin_Users_4_deleteallper",
                    "Admin/trang-chu/nguoi-dung/xoa-toan-bo-quyen-nhom-nguoi-dung-{id}",
                    new { controller = "RoleAndPermission", action = "DeleteAllPerFromGroup", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
                context.MapRoute(
                    "Admin_Users_4_detail",
                    "Admin/trang-chu/nguoi-dung/chi-tiet-nhom-nguoi-dung-{id}",
                    new { controller = "RoleAndPermission", action = "Details", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
                context.MapRoute(
                    "Admin_Users_4_addpertogroup",
                    "Admin/trang-chu/nguoi-dung/them-quyen-cho-nhom-nguoi-dung",
                    new { controller = "RoleAndPermission", action = "AddPerToGroup", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
                context.MapRoute(
                    "Admin_Users_4_deleteperfromgroup",
                    "Admin/trang-chu/nguoi-dung/xoa-quyen-cua-nhom-nguoi-dung-{UserGroupID}/{id}",
                    new { controller = "RoleAndPermission", action = "DeletePerFromGroup", id = UrlParameter.Optional },
                    new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
                );
            //group user end
            context.MapRoute(
                "Admin_Users_profile",
                "Admin/trang-chu/nguoi-dung/ho-so-nguoi-dung-{id}",
                new { controller = "Users", action = "UserProfile", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Users_editprofile",
                "Admin/trang-chu/nguoi-dung/sua-ho-so-nguoi-dung-{id}",
                new { controller = "Users", action = "EditProfile", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Users_edit_pass",
                "Admin/trang-chu/nguoi-dung/sua-mat-khau-nguoi-dung-{id}",
                new { controller = "Users", action = "EditPassword", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Users_delete",
                "Admin/trang-chu/nguoi-dung/xoa-nguoi-dung-{id}",
                new { controller = "Users", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Users END


            //Doanh thu
            context.MapRoute(
                "Admin_doanh_thu",
                "Admin/trang-chu/thong-ke/doanh-thu",
                new { controller = "ReportAndStatitic", action = "Profit", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_san_pham",
                "Admin/trang-chu/thong-ke/san-pham",
                new { controller = "ReportAndStatitic", action = "Product", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Doanh thu end

            //Er404
            context.MapRoute(
                "Admin_categories_404",
                "Admin/loi/{action}",
                new { controller = "Error", action = "Er404", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Er404 END

            //Er403
            context.MapRoute(
                "Admin_categories_403",
                "Admin/loi/{action}",
                new { controller = "Error", action = "Er403", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Er403 END

            //Er403
            context.MapRoute(
                "Admin_categories_ERORDERFALSE",
                "Admin/loi/{action}",
                new { controller = "Error", action = "ErOrderFalse", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
            //Er403 END

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebsiteBanSach.Areas.Admin.Controllers" }
            );
        }
    }
}