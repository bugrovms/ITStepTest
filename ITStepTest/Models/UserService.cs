using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void ActivateUserById(int id, bool active)
        {
            User user = db.Users.Find(id);
            if (user.FirstName != "Super" && user.LastName != "Administrator")
            {
                user.Active = active;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }            
        }
    }
}