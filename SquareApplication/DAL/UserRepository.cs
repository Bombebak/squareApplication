using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SquareApplication.Models;

namespace SquareApplication.DAL
{
    public class UserRepository : IDisposable
    {
        public SquaresEntities db = new SquaresEntities();

        public User GetUser(string name)
        {
            var user = db.Users.SingleOrDefault(u => u.name == name);
            return user;
        }

        public User GetUserFromId(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.user_id == id);
            return user;
        }
        public User GetUserById(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.user_id == id);
            return user;
        }

        public User GetUserFromMail(string email)
        {
            using (var dbContext = new SquaresEntities())
            {
                var user = dbContext.Users.SingleOrDefault(u => u.email == email);
                return user;
            }
        }
        public User GetUser(string email, string password)
        {
            using (var dbContext = new SquaresEntities())
            {
                var user = dbContext.Users.SingleOrDefault(u => u.email == email && u.password == password);
                return user;
            }
        }

        public ChangeProfileInfoViewModel GetUser(int userId)
        {
            var user = (from u in db.Users
                where u.user_id == userId
                select new ChangeProfileInfoViewModel()
                {
                    UserId = userId,
                    Address = u.address,
                    Name = u.name
                }).SingleOrDefault();
            return user;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}