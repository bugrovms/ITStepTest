using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class QuestionInformationModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Test { get; set; }
        public List<Variant> Variants { get; set; }
    }
}