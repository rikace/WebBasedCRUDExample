using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using TechStudioTest.Models;

namespace TechStudioTest.Auth
{
    public class CustomMembershipUser : MembershipUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomMembershipUser(User user) : base("CustomMembership", user.UserName, user.UserId, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}