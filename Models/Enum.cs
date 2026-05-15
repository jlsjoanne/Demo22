using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Demo22.Models.Enums
{
    public enum Identity
    {
        [Display(Name="未指派")]
        NotDecided = 0,
        [Display(Name="學生")]
        Student = 1,
        [Display(Name="老師")]
        Teacher = 2
    }
}