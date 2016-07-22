using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class MessageInformationModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SenderId { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public int RecipientId { get; set; }
        public bool Readed { get; set; }
    }
}