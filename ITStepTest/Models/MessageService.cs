using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class MessageService
    {
        private StoreDBEntities db = new StoreDBEntities();

        public int GetRecepientNotReadCount(int userId)
        {
            return db.Messages.Count(x => x.Recipient == userId && x.Readed == false);
        }
    }
}