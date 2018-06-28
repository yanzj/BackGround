using LtePlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    [ApiControl("应用程序用户管理控制器")]
    [Authorize]
    public class ApplicationUsersController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public ApplicationUsersController()
        {
            var context = ApplicationDbContext.Create();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
        }

        [HttpGet]
        [ApiDoc("获得目前所有用户信息列表")]
        [ApiResponse("应用程序中已注册的所有用户信息列表")]
        [Authorize(Roles = "管理员")]
        public IEnumerable<ApplicationUserViewModel> Get()
        {
            var context = ApplicationDbContext.Create();
            return context.Users.Select(x => new ApplicationUserViewModel
            {
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Hometown = x.Hometown,
                EmailHasBeenConfirmed = false
            });
        }

        [HttpGet]
        [ApiDoc("获得指定用户具有的角色列表")]
        public IEnumerable<string> Get(string userName)
        {
            var user = _userManager.FindByName(userName);
            if (user == null) return new List<string>();
            return _roleManager.Roles.ToList().Where(x => _userManager.IsInRole(user.Id, x.Name)).Select(x => x.Name);
        }
    }

    public class UserRolesController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public UserRolesController()
        {
            var context = ApplicationDbContext.Create();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
        }

        [HttpGet]
        public string Get(string userName)
        {
            var user = _userManager.FindByName(userName);
            if (user == null) return "";
            return
                string.Join(",",
                    _roleManager.Roles.ToList().Where(x => _userManager.IsInRole(user.Id, x.Name)).Select(x => x.Name).ToArray());
        }

    }
}
