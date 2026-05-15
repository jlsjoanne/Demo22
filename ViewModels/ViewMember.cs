using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Demo22.ViewModels
{
    public class ViewMember
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "帳號")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name= "名稱")]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}