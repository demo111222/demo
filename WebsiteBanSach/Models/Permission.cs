using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Permission
    {
        //Chi tiết quyền của các nhóm người dùng

        //mã nhóm người dùng
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Mã nhóm người dùng")]
        public string UserGroupID { get; set; }

        //mã quyền
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Mã quyền")]
        public string RoleID { get; set; }

        //Khai báo khóa ngoại
        public virtual UserGroup UserGroup { get; set; }
        public virtual Role Role { get; set; }
    }
}