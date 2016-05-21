using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SquareApplication.Interfaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string name, string password, string email, string address, bool isDesigner);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool GetUserById(int id);
    }
}