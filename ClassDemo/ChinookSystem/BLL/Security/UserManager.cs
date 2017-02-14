
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Chinook.Data.Enitities.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ChinookSystem.DAL.Security;
#endregion

namespace ChinookSystem.BLL.Security
{
    public class UserManager : UserManager<ApplicationUser>
    {
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        /// <summary>Requires FirstName and LastName</summary>
        private const string STR_USERNAME_FORMAT = "{0}{1}";
        /// <summary>Requires UserName</summary>
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ca";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        public void AddWebMaster()
        {
            //Users accesses all the records on the AspNetUsers table
            //UserName is the user logon user id (dwelch)
            if (!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                //create a new instance that will be used as the data to
                //   add a new record to the AspNetUsers table
                //dynamically fill two attributes of the instance
                var webmasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };

                //place the webmaster account on the AspNetUsers table
                this.Create(webmasterAccount, STR_DEFAULT_PASSWORD);

                //place an account role record on the AspNetUserRoles table
                //.Id comes from the webmasterAccount and is the pkey of the Users table
                //role will comes from the Entities.Security.SecurityRoles
                this.AddToRole(webmasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }
    }

}
