using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SquareApplication.Models;

namespace SquareApplication.DAL
{
    public class UserRepository
    {
        public SquaresEntities db = new SquaresEntities();

        public User GetUser(string userName)
        {
            var user = db.Users.SingleOrDefault(u => u.name == userName);
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
        public User GetUser(string userName, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.name == userName && u.password == password);
            return user;
        }
    }
}