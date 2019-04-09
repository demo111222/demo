using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class UserGroup
    {
        //mã nhóm người dùng
        [Key]
        [Display(Name = "Mã nhóm người dùng")]
        public string ID { get; set; }

        //Tên nhóm người dùng
        [StringLength(250, ErrorMessage = "Tên nhóm người dùng không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống tên nhóm người dùng")]
        [Display(Name = "Tên nhóm người dùng")]
        public string Name { get; set; }

        //UserGroup có nhiều User
        public ICollection<User> Users { get; set; }

        //UserGroup có nhiều Role và ngược lại
        public ICollection<Permission> Permissions { get; set; }
    }
}