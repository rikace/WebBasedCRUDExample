using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using TechStudioTest.DataAccess;

namespace TechStudioTest.Auth
{
    public class CustomMembership : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            using (AuthContext authContext = new AuthContext())
            {
                var user = (from u in authContext.Users
                            where string.Compare(username, u.UserName, StringComparison.OrdinalIgnoreCase) == 0
                            && string.Compare(password, u.Password, StringComparison.OrdinalIgnoreCase) == 0
                            && u.IsActive == true
                            select u).FirstOrDefault();
                return (user != null) ? true : false;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (AuthContext authContext = new AuthContext())
            {
                var user = (from u in authContext.Users
                            where string.Compare(username, u.UserName, StringComparison.OrdinalIgnoreCase) == 0
                            select u).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                var selectedUser = new CustomMembershipUser(user);
                return selectedUser;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (AuthContext authContext = new AuthContext())
            {
                string username = (from u in authContext.Users
                                   where string.Compare(email, u.Email) == 0
                                   select u.UserName).FirstOrDefault();
                return !string.IsNullOrEmpty(username) ? username : string.Empty;
            }
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
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
    }
}