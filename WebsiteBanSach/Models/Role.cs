using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Role
    {
        //mã quyền
        [Key]
        [Display(Name = "Mã quyền")]
        public string ID { get; set; }

        //Tên nhóm người dùng
        [StringLength(250, ErrorMessage = "Tên quyền không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống tên quyền")]
        [Display(Name = "Tên quyền")]
        public string Name { get; set; }

        //Role có nhiều UserGroup và ngược lại
        public ICollection<Permission> Permissions { get; set; }
    }
}