using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Reflection;
using static Demo22.Models.EmumList;

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
        [Display(Name="性別")]
        public Gender Gender { get; set; }

        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "權限")]
        [MaxLength(500)]
        public string Permission { get; set; }

        
    }
}