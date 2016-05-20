using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SquareApplication.Interfaces;

namespace SquareApplication.Services
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Brugernavnet kan ikke være tomt.", "userName");

            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Adgangskoden kan ikke være tom.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string name, string password, string email, string address, bool isDesigner)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Brugernavnet kan ikke være tomt.", "name");
            if (name.Contains(";")) throw new ArgumentException("Brugernavnet må ikke indeholde ';'", "name");
            if (name.Contains("*")) throw new ArgumentException("Brugernavnet må ikke indeholde '*'", "name");
            if (name.Contains(")")) throw new ArgumentException("Brugernavnet må ikke indeholde ')'", "name");
            if (name.Contains("(")) throw new ArgumentException("Brugernavnet må ikke indeholde '('", "name");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Adgangskoden kan ikke være tom.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Du skal indtaste en e-mail.", "email");

            MembershipCreateStatus status;
            // ORIGINAL: 6th Parameter is IsApproved property - which defaults to true
            //_provider.CreateUser(userName, password, email, null, null, true, null, out status);
            // MODIFICATION: Set the IsApproved property to false
            _provider.CreateUser(name, password, email, address, null, isDesigner, null, out status);
            return status;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Brugernavnet kan ikke være tomt.", "userName");
            if (userName.Contains(";")) throw new ArgumentException("Brugernavnet må ikke indeholde ';'", "userName");
            if (userName.Contains("*")) throw new ArgumentException("Brugernavnet må ikke indeholde '*'", "userName");
            if (userName.Contains(")")) throw new ArgumentException("Brugernavnet må ikke indeholde ')'", "userName");
            if (userName.Contains("(")) throw new ArgumentException("Brugernavnet må ikke indeholde '('", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Adgangskoden kan ikke være tom.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Du skal indtaste en e-mail.", "email");

            MembershipCreateStatus status;
            // ORIGINAL: 6th Parameter is IsApproved property - which defaults to true
            //_provider.CreateUser(userName, password, email, null, null, true, null, out status);
            // MODIFICATION: Set the IsApproved property to false
            _provider.CreateUser(userName, password, email, null, null, false, null, out status);
            return status;
        }



        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }



        

        public bool GetUserById(int id)
        {
            if (id != null)
            {

            }
            return false;
        }
    }
}