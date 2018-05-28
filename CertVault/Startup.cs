using CertVault.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
//using log4net;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(CertVault.Startup))]
namespace CertVault
{
    public partial class Startup
    {
        private static readonly List<ApplicationUser> AdminUsers = new List<ApplicationUser>();
        private static readonly List<ApplicationUser> Clerks = new List<ApplicationUser>();
        private static readonly List<ApplicationUser> Supervisors = new List<ApplicationUser>();
        private static readonly List<ApplicationUser> Managers = new List<ApplicationUser>();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administrators"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrators";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                AdminUsers.Add(new ApplicationUser() { UserName = "Admin", Email = "jseitgroup@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "peter.robinson", Email = "peter.robinson@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "james.duncan", Email = "james.duncan@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "roland.lattery", Email = "roland.lattery@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "sanjay.campbell", Email = "sanjay.campbell@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "suzette.mcnaught", Email = "suzette.mcnaught@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "tafari.johnson", Email = "tafari.Johnson@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "migration.tool", Email = "migration.tool@jamstockex.com" });
                AdminUsers.Add(new ApplicationUser() { UserName = "xavier.watson", Email = "xavier.watson@jamstockex.com" });
                string userPWD = "JSEL0ca!";

                foreach (var user in AdminUsers)
                {
                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Administrators");
                    }
                }

            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

                Managers.Add(new ApplicationUser() { UserName = "kadyll.mcnaught", Email = "kadyll.mcnaught@jamstockex.com" });
                string userPWD = "P@ssw0rd";

                foreach (var user in Managers)
                {
                    var chkUser = UserManager.Create(user, userPWD);
                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Manager");
                    }
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Supervisor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);
                
                Supervisors.Add(new ApplicationUser() { UserName = "annmarie.murphy", Email = "annmarie.murphy@jamstockex.com" });
                Supervisors.Add(new ApplicationUser() { UserName = "suzette.pryce", Email = "suzette.pryce@jamstockex.com" });
                string userPWD = "P@ssw0rd";

                foreach (var user in Supervisors)
                {
                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Supervisor");
                    }
                }
            }

            // creating Creating Clerk role    
            if (!roleManager.RoleExists("Clerk"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Clerk";
                roleManager.Create(role);

                Clerks.Add(new ApplicationUser() { UserName = "melesia.otto", Email = "melesia.otto@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "sheldon.stennett", Email = "sheldon.stennett@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "simon.johnson", Email = "simon.johnson@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "tracey.wynter", Email = "tracey.wynter@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "trudiann.williams", Email = "trudiann.williams@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "lando.graham", Email = "lando.graham@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "glenville.hamilton", Email = "glenville.hamilton@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "alecia.harrison", Email = "alecia.harrison@jamstockex.com" });
                Clerks.Add(new ApplicationUser() { UserName = "adrian.jackson", Email = "adrian.jackson@jamstockex.com" });
                string userPWD = "P@ssw0rd";

                foreach (var user in Clerks)
                {
                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Clerk");
                    }
                }
            }
        }
    }
}
