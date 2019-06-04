using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class RoleAndPermissionController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        //danh sach nhom nguoi dung
        public ActionResult UserGroups()
        {
            var u = data.UserGroups.ToList();
            //xác nhận quyền
            var t = "VIEW_USERGROUP";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //danh sách user
            ViewBag.User = data.Users.ToList();
            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }
        //danh sach nhom nguoi dung end

        //danh sách quyền
        public ActionResult Role()
        {
            var u = data.Roles.ToList();
            //xác nhận quyền
            var t = "VIEW_ROLE";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //danh sách user
            ViewBag.User = data.Users.ToList();
            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }
        //danh sách quyền end

        //chi tiet nhóm người dùng và các quyền mà nhóm này có
        public ActionResult Details(String id)
        {
            //xác nhận quyền
            var t = "VIEW_USERGROUP";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var ug = data.UserGroups.Single(c => c.ID == id);
            //danh sách CHi tiết quyền của nhóm
            ViewBag.listPer = data.Permissions.Where(c => c.UserGroupID == ug.ID).ToList();
            //danh sách quyền
            ViewBag.listRole = data.Roles.ToList();
            if (ug == null)
            {
                return RedirectToAction("Er404", "loi/");
            }

            return View(ug);
        }
        //chi tiet nhóm người dùng và các quyền mà nhóm này có end

        //them quyền cho nhóm
        public ActionResult AddPerToGroup()
        {
            //xác nhận quyền
            var t = "ADD_PERMISSIONS";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            ViewBag.ListUGroup = data.UserGroups.ToList();
            ViewBag.UserGroupID = new SelectList(data.UserGroups.ToList().OrderBy(n => n.Name), "ID", "Name");
            ViewBag.RoleID = new SelectList(data.Roles.ToList().OrderBy(n => n.Name), "ID", "Name");
            return View();
        }

        // POST: Admin/RoleAndPermission/Create
        [HttpPost]
        public ActionResult AddPerToGroup(Permission a)
        {
            try
            {
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();

                //chặn thêm ctquyền  đã có trong hệ thống
                var listpermission = data.Permissions.ToList();
                foreach (var obj in listpermission)
                {
                    if(a==obj)
                    {
                        ViewBag.Error = "Nhóm đã có quyền này";
                        return View();
                    }
                }

                //chặn không cho thêm các quyền EDIT, DELETE liên quan đến ORDER,PERMISSION cho mod, standby
                
                //StandbyAccount
                if (a.UserGroupID == "StandbyAccount")
                {
                    ViewBag.Error = "Nhóm này là nhóm mặc định không thể thêm quyền";
                    return View();
                }
                if (a.UserGroupID == "Member")
                {
                    ViewBag.Error = "Nhóm khách hàng là nhóm mặc định không thể thêm quyền";
                    return View();
                }
                //mod
                string[] editanddelete = { "EDIT_ORDER", "EDIT_USER", "EDIT_USERGROUP", "DELETE_ORDER", "DELETE_USER", "DELETE_USERGROUP" };
                string[] add = {"ADD_ORDER","ADD_USER"};
                if (a.UserGroupID == "Mod" )
                {
                    foreach(var value in editanddelete)
                    {
                        if(a.RoleID==value)
                        {
                            ViewBag.Error = "Quyền mặc định của nhóm Admin không thể thêm cho nhóm nào khác";
                            return View();
                        }
                    }
                    
                }//mod end
                //kiểm tra cho Admin -> Admin ko đc thêm quyền cho bản thân
                var t4 = Session["UserGroup"].ToString();
                if(a.UserGroupID==t4)
                {
                    ViewBag.Error = "Quyền này mặc định của nhóm khách hàng (Member)";
                    return View();
                }
                //kiểm tra end
                if (a == null)
                {
                    ViewBag.Error = "Bạn vẫn chưa chọn nhóm hay quyền nào cả";
                }
                if (a.RoleID == null && a.UserGroupID != null)
                {
                    ViewBag.Error = "Bạn chưa chọn quyền để thêm cho nhóm";
                }
                else
                {
                    if (a.RoleID != null && a.UserGroupID == null)
                    {
                        ViewBag.Error = "Bạn chưa chọn nhóm để thêm quyền";
                    }
                    else
                    {
                        ViewBag.Error = "Bạn vẫn chưa chọn nhóm hay quyền nào cả";
                    }
                }
                if (ModelState.IsValid)
                {
                    data.Permissions.Add(a);
                    data.SaveChanges();
                }

                var listu = data.Users.Where(c => c.UserGroupID == a.UserGroupID);
                foreach(var item in listu)
                {
                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content1 = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Addpertogpassive.html"));

                    content1 = content1.Replace("{{Role}}", a.Role.Name);
                    content1 = content1.Replace("{{UserGroup}}", a.UserGroup.ID);
                    content1 = content1.Replace("{{UserName}}", t1);
                    content1 = content1.Replace("{{Phone}}", t2);
                    content1 = content1.Replace("{{Email}}", t3);

                    new MailHelper().SendMail(item.Email, "Tác giả đã được xóa thành công", content1);
                }

                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Addpertog.html"));

                content = content.Replace("{{Role}}", a.Role.Name);
                content = content.Replace("{{UserGroup}}", a.UserGroup.ID);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Tác giả đã được xóa thành công", content);

                return RedirectToAction("Details", new { id = a.UserGroupID.ToString() });
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //them quyền cho nhóm end

        //xóa quyền của nhóm 
        public ActionResult DeletePerFromGroup(string UserGroupID,string id)
        {
            //xác nhận quyền
            var t = "DELETE_PERMISSIONS";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //xác nhận quyền end

            var per = data.Permissions.Single(C => C.UserGroupID == UserGroupID && C.RoleID == id);

            if(per==null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(per);
        }
        [HttpPost, ActionName("DeletePerFromGroup")]
        public ActionResult ConfirmDeletePerFromGroup(string UserGroupID, String id)
        {
            try
            {
                //kiểm tra cho Admin -> Admin ko đc xóa quyền cho bản thân
                var tt = Session["UserGroup"].ToString();
                if (UserGroupID == tt)
                {
                    Response.Redirect("/Admin/loi/Er403");
                }
                //không cho xóa quyền của admin
                var permission = data.Permissions.Single(C => C.UserGroupID == UserGroupID && C.RoleID == id);
                if(permission.UserGroupID=="Admin" || permission.UserGroupID == "StandbyAccount"||permission.UserGroupID == "Member")
                {
                    ViewBag.Error = "Quyền mặc định của nhóm không thể bị xóa";
                    return RedirectToAction("DeletePerFromGroup", new { UserGroupID=permission.UserGroupID, id=permission.RoleID});
                }
                //kiểm tra end
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();

                var per = data.Permissions.Single(C => C.UserGroupID == UserGroupID && C.RoleID == id);

                if (per == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                
                data.Permissions.Remove(per);
                data.SaveChanges();

                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Dele_per_from_ugroup.html"));

                content = content.Replace("{{RoleName}}", id);
                content = content.Replace("{{UserGroup}}", UserGroupID);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Tác giả đã được xóa thành công", content);

                //vòng lập gửi mail cho DANH SÁCH NGƯỜI DÙNG THUỘC NHÓM bị xóa quyền
                var ListUser = data.Users.Where(c => c.UserGroupID == UserGroupID).ToList();
                if(ListUser!=null)//nếu danh sách có tài khoản thuộc nhóm này
                {
                    foreach (var item in ListUser)
                    {
                        string mail = item.Email;

                        string content1 = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Dele_per_from_ugroup.html"));

                        content1 = content1.Replace("{{RoleName}}", id);
                        content1 = content1.Replace("{{UserGroup}}", UserGroupID);
                        content1 = content1.Replace("{{UserName}}", t1);
                        content1 = content1.Replace("{{Phone}}", t2);
                        content1 = content1.Replace("{{Email}}", t3);
                        new MailHelper().SendMail(mail, "Quyền đã được xóa thành công", content1);
                    }
                }
                //vòng lập gửi mail cho DANH SÁCH NGƯỜI DÙNG THUỘC NHÓM bị xóa quyền end

                //gửi mail vào sau khi thay đổi thành công dữ liệu END

                return RedirectToAction("Details",new { id= UserGroupID });
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //xóa quyền của nhóm end

        //add nhóm
        public ActionResult Create()
        {
            //xác nhận quyền
            var t = "ADD_USERGROUP";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //xác nhận quyền end
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(UserGroup ug)
        {
            try
            {
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();//end

                var list = data.UserGroups.ToList();
                
                if (ug.ID == null)
                {
                    ViewBag.Error = "Không được để trống mã nhóm";
                    return View();
                }
                else
                {
                    int length = ug.ID.Length;
                    if(length>250)
                    {
                        ViewBag.Error = "Mã nhóm không được vượt 250 ký tự";
                        return View();
                    }
                }
                if (ug.Name == null)
                {
                    ViewBag.Error = "Không được để trống tên nhóm";
                    return View();
                }
                else
                {
                    int length = ug.Name.Length;
                    if (length > 250)
                    {
                        ViewBag.Error = "Tên nhóm không được vượt 250 ký tự";
                        return View();
                    }
                }
                foreach (var item in list)
                {
                    if(item.ID==ug.ID)
                    {
                        ViewBag.Error = "Nhóm người dùng đã tồn tại vui lòng chọn mã nhóm khác";
                        return View();
                    }
                    if(item.Name==ug.Name)
                    {
                        ViewBag.Error = "Nhóm người dùng đã tồn tại vui lòng chọn tên nhóm khác";
                        return View();
                    }
                }
                
                if (ModelState.IsValid)
                {
                    data.UserGroups.Add(ug);
                    data.SaveChanges();
                }

                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Create_ug.html"));


                content = content.Replace("{{UserGroupID}}", ug.ID);
                content = content.Replace("{{UserGroupName}}", ug.Name);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Nhóm người dùng đã được thêm thành công", content);
                //end

                return RedirectToAction("AddPerToGroup");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //add nhóm

        //sửa nhóm
        public ActionResult Edit(String id)
        {
            //xác nhận quyền
            var t = "EDIT_USERGROUP";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //xác nhận quyền end

            var ug = data.UserGroups.Single(C => C.ID == id);

            if (ug == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(ug);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(String id,UserGroup ug)
        {
            try
            {
                var temp = data.UserGroups.Single(u => u.ID == id);
                ug = data.UserGroups.Single(u => u.ID == id);
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();//end

                var list = data.UserGroups.ToList();
                if (ug == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                if (ug.ID == null || ug.ID != null)
                {
                    ug.ID = temp.ID;
                }
                else
                {
                    int length = ug.ID.Length;
                    if (length > 250)
                    {
                        ViewBag.Error = "Mã nhóm không được vượt 250 ký tự";
                        return View();
                    }
                }
                if (ug.Name == null)
                {
                    ViewBag.Error = "Không được để trống tên nhóm";
                    return View();
                }
                else
                {
                    int length = ug.Name.Length;
                    if (length > 250)
                    {
                        ViewBag.Error = "Tên nhóm không được vượt 250 ký tự";
                        return View();
                    }
                }

                if (ModelState.IsValid)
                {
                    if(ug.ID!=temp.ID)
                    {
                        ug.ID = temp.ID;
                    }
                    UpdateModel(ug);
                    data.SaveChanges();
                }
                
                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Edit_ug.html"));


                content = content.Replace("{{UserGroupID}}", ug.ID);
                content = content.Replace("{{UserGroupName}}", ug.Name);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Nhóm người dùng đã được thêm thành công", content);
                //end

                return RedirectToAction("UserGroups");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //sửa nhóm end

        // xóa nhóm
        public ActionResult Delete(String id)
        {
            //xác nhận quyền
            var t = "DELETE_USERGROUP";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //xác nhận quyền end

            var ug = data.UserGroups.Single(C => C.ID == id);

            if (ug == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(ug);
        }

        // Xóa nhóm
        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(String id)
        {
            try
            {
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();
                

                var listU = data.Users.Where(C => C.UserGroupID == id).ToList(); //danh sách người dùng thuộc nhóm bị xóa
                var ug = data.UserGroups.Single(C => C.ID == id);

                if (ug == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                //Nếu nhóm bị xóa là 4 nhóm cơ bản của hệ thống
                if (ug.ID=="Member" || ug.ID =="Admin" || ug.ID =="Mod" || ug.ID== "StandbyAccount")
                {
                    ViewBag.Error = "Nhóm người dùng này là mặc định bạn không có quyền xóa";
                    return RedirectToAction("Delete", new { id = ug.ID });
                }

                //xóa per
                var listPer = data.Permissions.Where(C => C.UserGroupID == ug.ID);
                if (listPer != null)
                {
                    foreach (var per in listPer)
                    {
                        data.Permissions.Remove(per);
                    }
                }
                
                //xóa per end

                //đổi người thuộc nhóm bị xóa  sang  standby
                if(listU!=null)
                {
                    foreach (var item in listU)
                    {
                        item.UserGroupID = "StandbyAccount";
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedBy = t1;

                        //gửi mail về thay đổi
                        string content1 = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Delete_ug_passive.html"));


                        content1 = content1.Replace("{{UserGroupOld}}", id);
                        content1 = content1.Replace("{{UserGroupNew}}", "StandbyAccount");
                        content1 = content1.Replace("{{UserName}}", t1);
                        content1 = content1.Replace("{{Phone}}", t2);
                        content1 = content1.Replace("{{Email}}", t3);

                        new MailHelper().SendMail(item.Email, "Nhóm người dùng đã được xóa thành công", content1);
                        //gửi mail về thay đổi end
                    }
                }
                //Nếu nhóm bị xóa là 4 nhóm cơ bản của hệ thống end

                data.UserGroups.Remove(ug);
                data.SaveChanges();

                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Delete_ug.html"));

                
                content = content.Replace("{{UserGroup}}", id);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Nhóm người dùng đã được xóa thành công", content);
                //end

                return RedirectToAction("UserGroups");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        //xóa toàn bộ per
        public ActionResult DeleteAllPerFromGroup(string id)
        {
            //xác nhận quyền
            var t = "DELETE_PERMISSIONS";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            //xác nhận quyền end

            //danh sách CHi tiết quyền của nhóm
            ViewBag.listPer = data.Permissions.Where(c => c.UserGroupID == id).ToList();
            //danh sách quyền
            ViewBag.listRole = data.Roles.ToList();

            var per = data.UserGroups.Single(C => C.ID == id);

            if (per == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(per);
        }
        [HttpPost, ActionName("DeleteAllPerFromGroup")]
        public ActionResult ConfirmDeleteAllPerFromGroup(String id)
        {
            try
            {
                var temp = id;
                //kiểm tra cho Admin -> Admin ko đc xóa quyền cho bản thân
                var tt = Session["UserGroup"].ToString();
                if (id == tt)
                {
                    Response.Redirect("/Admin/loi/Er403");
                }
                //kiểm tra end
                //biến dùng trong việc gửi mail
                var t1 = Session["UserName"].ToString();
                var t2 = Session["Phone"].ToString();
                var t3 = Session["Email"].ToString();

                //Nếu nhóm bị xóa là 4 nhóm cơ bản của hệ thống
                if (id== "Member" || id== "Admin" || id == "Mod" || id == "StandbyAccount")
                {
                    ViewBag.Error = "Nhóm người dùng này có một vài quyền không thể bị xóa";
                    return RedirectToAction("DeleteAllPerFromGroup", new { id = temp });
                }

                var per = data.Permissions.Where(C => C.UserGroupID==id).ToList();

                if (per == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                foreach(var perm in per)
                {
                    data.Permissions.Remove(perm);
                }
                data.SaveChanges();

                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Dele_per_from_ugroup.html"));

                content = content.Replace("{{RoleName}}", "tất cả các quyền");
                content = content.Replace("{{UserGroup}}", id);
                content = content.Replace("{{UserName}}", t1);
                content = content.Replace("{{Phone}}", t2);
                content = content.Replace("{{Email}}", t3);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Tác giả đã được xóa thành công", content);

                //vòng lập gửi mail cho DANH SÁCH NGƯỜI DÙNG THUỘC NHÓM bị xóa quyền
                var ListUser = data.Users.Where(c => c.UserGroupID == id).ToList();
                if (ListUser != null)//nếu danh sách có tài khoản thuộc nhóm này
                {
                    foreach (var item in ListUser)
                    {
                        string mail = item.Email;

                        string content1 = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/Dele_per_from_ugroup.html"));

                        content1 = content1.Replace("{{RoleName}}", "tất cả các quyền");
                        content1 = content1.Replace("{{UserGroup}}", id);
                        content1 = content1.Replace("{{UserName}}", t1);
                        content1 = content1.Replace("{{Phone}}", t2);
                        content1 = content1.Replace("{{Email}}", t3);
                        new MailHelper().SendMail(mail, "Quyền đã được xóa thành công", content1);
                    }
                }
                //vòng lập gửi mail cho DANH SÁCH NGƯỜI DÙNG THUỘC NHÓM bị xóa quyền end

                //gửi mail vào sau khi thay đổi thành công dữ liệu END

                return RedirectToAction("Details", new { id = temp });
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //xóa toàn bộ per end
    }
}
