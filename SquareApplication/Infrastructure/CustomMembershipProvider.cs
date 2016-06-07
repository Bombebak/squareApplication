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
            var args = new ValidatePasswordEventArgs(email, password, true);
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

            var user = GetUser(email, true);

            if (user == null)
            {
                using (var dbContext = new SquareDbEntities())
                {
                    var userObj = new User { name = username, password = password, email = email, address = passwordQuestion };

                    dbContext.Users.Add(userObj);

                    //This is a hack
                    if (isApproved)
                    {
                        var role = dbContext.Roles.SingleOrDefault(r => r.name == "Designer");
                        if (role != null)
                        {
                            var userRole = new UserRole()
                            {
                                role_id = role.role_id,
                                user_id = userObj.user_id
                            };
                            dbContext.UserRoles.Add(userRole);
                        }
                    }
                    else
                    {
                        var role = dbContext.Roles.SingleOrDefault(r => r.name == "User");
                        if (role != null)
                        {
                            var userRole = new UserRole()
                            {
                                role_id = role.role_id,
                                user_id = userObj.user_id
                            };
                            dbContext.UserRoles.Add(userRole);
                        }
                    }
                    dbContext.SaveChanges();
                }
                status = MembershipCreateStatus.Success;

                return GetUser(email, true);
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

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = userRepository.GetUserFromMail(email);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", email, user.user_id, user.email,
                                                            string.Empty, string.Empty,
                                                            true, false, DateTime.Now, //Registered
                                                             DateTime.Now, //lastLogin
                                                            DateTime.MinValue,
                                                            DateTime.MinValue, DateTime.MinValue);
                return memUser;
            }
            return null;
        }

        public MembershipUser GetUser(int providerUserKey, bool userIsOnline)
        {
            var user = userRepository.GetUserById(providerUserKey);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", user.name, user.user_id, user.email,
                                                            string.Empty, string.Empty,
                                                            false, false, DateTime.Now, //Registered
                                                            DateTime.Now, //LastLogin
                                                            DateTime.MinValue,
                                                            DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        public MembershipUser GetUserFromId(int id, bool userIsOnline)
        {
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

        public override string GetUserNameByEmail(string email)
        {
            using (SquareDbEntities db = new SquareDbEntities())
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



        public override bool ValidateUser(string email, string password)
        {
            var requiredUser = userRepository.GetUser(email, password);
            return requiredUser != null;
        }
    }
}