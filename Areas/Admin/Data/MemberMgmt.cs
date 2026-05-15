using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo22.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo22.Areas.Admin.Data
{
    public class MemberMgmt
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="帳號")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "身分")]
        public Identity Identity { get; set; }
    }
}