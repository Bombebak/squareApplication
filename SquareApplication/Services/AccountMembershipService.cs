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

        public bool RetrievePassword(string email)
        {
            //if (email == null) throw new ArgumentException("Email kan ikke være tom.", "email");
            //try
            //{
            //    MembershipUser currentUser = _provider.GetUser(email, true);
            //    return currentUser;
            //}
            throw new NotImplementedException();
        }

        public void SendConfirmationEmail(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            int confirmationGuid = Int32.Parse(user.ProviderUserKey.ToString());
            string verifyUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                             "/account/verify?ID=" + confirmationGuid;
            string body = string.Format("<h2>Velkommen {0}</h2>" +
                                         "<p><br/>Tak for din tilmeldelse på vores hjemmeside.</p>" +
                                         "<p>Før du er helt færdig med oprerttelsen, skal du trykke på linket, som sender dig over til vores hjemmeside.</p>" +
                                          "<p><a href=\"{1}\"title=\"User Email Confirm\">{1}</a></p>" +
                                         "<p>På vores hjemmeside kan du holde dig opdateret med følgende emner:" +
                                         "<ul style='font-size: 15px;'>" +
                                         "<li><a href='http://ebeltoft-cykelmotion.dk/Article'>Nyheder</a></li>" +
                                         "<li><a href='http://ebeltoft-cykelmotion.dk/Team/Training'>Træningstidspunkter</a></li>" +
                                         "<li><a href='http://ebeltoft-cykelmotion.dk/Team/OurTeams'>Hold kaptajner</a></li>" +
                                         "<li><a href='http://ebeltoft-cykelmotion.dk/Forum'>Forum</a></li>" +
                                         "<li><a href='http://ebeltoft-cykelmotion.dk/Image'>Galleri</a></li>" +
                                         "</ul></p>" +
                                        "<p>Har du ikke tilmeldt dig på vores hjemmeside, så kan du bare ignore denne besked.</p>" +
                                         "<p>Mange hilsner</p>" +
                                         "<p>Ebeltoft-Cykelmotion</p>" +
                                         "<p><img src='~/Images/Logo/TheRealOneLogo.png'</img>", userName, verifyUrl

                                        );


            var message = new System.Net.Mail.MailMessage("webmaster@ebeltoft-cykelmotion.dk", user.Email)
            {
                Subject = "Bekræftelse af mail",
                Body = body,
                IsBodyHtml = true
            };


            var client = new System.Net.Mail.SmtpClient();

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                e.Message.ToString();
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