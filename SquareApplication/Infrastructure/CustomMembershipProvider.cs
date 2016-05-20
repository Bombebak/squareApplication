using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using SquareApplication.DAL;
using SquareApplication.Models;

namespace SquareApplication.Infrastructure
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private UserRepository userRepository = new UserRepository();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //http://theintegrity.co.uk/2010/12/asp-net-mvc-2-custom-membership-provider-tutorial-part-3/

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword))
                return false;
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var user = GetUser(username, true);

            if (user == null)
            {
                var userObj = new User { name = username, password = GetMd5Hash(password), email = email, address = ""};

                using (var usersContext = new SquaresEntities())
                {
                    usersContext.Users.Add(userObj);
                }

                status = MembershipCreateStatus.Success;

                return GetUser(username, true);
            }
            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {

            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var usersContext = new SquaresEntities();
            var user = userRepository.GetUser(username);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", username, user.user_id, user.email,
                                                            string.Empty, string.Empty,
                                                            true, false, DateTime.Now,
                                                             DateTime.Now, //lastLogin
                                                            DateTime.MinValue,
                                                            DateTime.MinValue, DateTime.MinValue);
                return memUser;
            }
            return null;
        }

        public MembershipUser GetUser(int providerUserKey, bool userIsOnline)
        {
            var usersContext = new SquaresEntities();
            var user = userRepository.GetUserById(providerUserKey);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", user.name, user.user_id, user.email,
                                                            string.Empty, string.Empty,
                                                            false, false, DateTime.Now, //Registered
                                                            DateTime.Now , //LastLogin
                                                            DateTime.MinValue,
                                                            DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        public MembershipUser GetUserFromId(int id, bool userIsOnline)
        {
            var usersContext = new SquaresEntities();
            var user = userRepository.GetUserFromId(id);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", user.name, user.user_id, user.email,
                                                            string.Empty, string.Empty,
                                                            true, false, DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        //public MembershipUser GetUserFromMail(string email, bool userIsOnline)
        //{
        //    var usersContext = new SquaresEntities();
        //    var user = usersContext.GetUserFromMail(email);
        //    if (user != null)
        //    {
        //        var memUser = new MembershipUser("CustomMembershipProvider", email, user.id, user.email,
        //                                                    string.Empty, string.Empty,
        //                                                    user.EmailConfirmed, false, (DateTime) user.Registered,
        //                                                    (DateTime) user.LastLogin,
        //                                                    DateTime.MinValue,
        //                                                    DateTime.Now, DateTime.Now);
        //        return memUser;
        //    }
        //    return null;
        //}

        public override string GetUserNameByEmail(string email)
        {
            using (SquaresEntities db = new SquaresEntities())
            {
                var result = from u in db.Users where (u.email == email) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.FirstOrDefault();

                    return dbuser.name;
                }
                //else
                //{
                //    throw new ArgumentException("Email findes ikke.", "email");
                //}
            }
            return "";
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {

            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }



        public override bool ValidateUser(string username, string password)
        {
            var md5Hash = GetMd5Hash(password);

            using (var usersContext = new SquaresEntities())
            {
                var requiredUser = userRepository.GetUser(username, password);
                return requiredUser != null;
            }
        }

        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}