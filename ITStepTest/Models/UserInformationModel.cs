using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class UserInformationModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateCreate { get; set; }
        public System.DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public string Group { get; set; }
        public bool Active { get; set; }
    }
}