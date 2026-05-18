using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Demo22.Models.Enums;
using System.Reflection;

namespace Demo22.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "帳號")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "名稱")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public Identity Identity { get; set; }

        [Display(Name="身分")]
        public string IdentityDisplayName
        {
            get
            {
                var field = Identity.GetType().GetField(Identity.ToString());
                if (field != null)
                {
                    var attr = field.GetCustomAttribute<DisplayAttribute>();
                    if(attr != null)
                    {
                        return attr.GetName();
                    }
                }
                return Identity.ToString();
            }
        }
    }
}