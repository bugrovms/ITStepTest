using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITStepTest.Enums
{
    public enum RolesType : int
    {
        [Display(Name = "Студент")]
        User = 0,
        [Display(Name = "Преподаватель")]
        Teacher = 1,
        [Display(Name = "Администратор")]
        Administrator = 0,
    }
}