using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITStepTest.Models
{
    public class UserService
    {

        private StoreDBEntities db = new StoreDBEntities();

        public IEnumerable<User> GetAll() {
            return db.Users.ToList();
        }
        public User GetByName(string userName)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == userName);
            return user;
        }
    }
}