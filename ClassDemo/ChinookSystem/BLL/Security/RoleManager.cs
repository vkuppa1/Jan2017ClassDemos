
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ChinookSystem.DAL.Security;
#endregion

namespace ChinookSystem.BLL.Security
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager() : base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {
        }

        public void AddStartupRoles()
        {
            foreach (var roleName in SecurityRoles.DefaultSecurityRoles)
            {
                //Roles accesses all the records on the AspNetRoles table
                //Name is the role (WebsiteAdmins)
                if (!Roles.Any(r => r.Name.Equals(roleName)))
                {
                    //create a new instance that will be used as the data to 
                    //   add a new record to the AspNetRoles table
                    //add role name to the instance
                    this.Create(new IdentityRole(roleName));
                }
            }
        }
    }
}
