using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class CommentInformationModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int User { get; set; }
        public int Role { get; set; }
        public string UserFullName { get; set; }
        public int Test { get; set; }
        public string TestName { get; set; }
        public int Subject { get; set; }
        public string SubjectName { get; set; }
        public System.DateTime Date { get; set; }
    }
}