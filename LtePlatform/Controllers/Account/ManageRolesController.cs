using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    [Authorize(Roles = "管理员")]
    [ApiControl("增加角色下用户控制器")]
    public class ManageRolesController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public ManageRolesController()
        {
            var context = ApplicationDbContext.Create();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
        }

        [HttpGet]
        [ApiDoc("解除角色和用户之间的绑定关系")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiParameterDoc("userName", "用户名称")]
        [ApiResponse("解除是否成功")]
        public bool Get(string roleName, string userName)
        {
            if (!_roleManager.RoleExists(roleName)) return false;
            var user = _userManager.FindByName(userName);
            if (user == null) return false;
            if (!_userManager.IsInRole(user.Id, roleName)) return false;
            _userManager.RemoveFromRole(user.Id, roleName);
            return true;
        }

        [HttpPost]
        [ApiDoc("批量向用户删除某个角色")]
        [ApiParameterDoc("dto", "角色删除信息，包含角色名称和待删除的用户列表")]
        [ApiResponse("删除是否成功")]
        public bool Post(RoleUsersDto dto)
        {
            if (!_roleManager.RoleExists(dto.RoleName)) return false;
            foreach (
                var user in
                dto.UserNames.Select(userName => _userManager.FindByName(userName))
                    .Where(user => user != null)
                    .Where(user => _userManager.IsInRole(user.Id, dto.RoleName)))
            {
                _userManager.RemoveFromRole(user.Id, dto.RoleName);
            }
            return true;
        }

        [HttpGet]
        [ApiDoc("查询指定角色下带的用户列表，以便减少角色")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiResponse("指定角色下带的用户列表")]
        public IEnumerable<ApplicationUser> Get(string roleName)
        {
            if (!_roleManager.RoleExists(roleName)) return null;
            return _userManager.Users.Where(user => _userManager.IsInRole(user.Id, roleName));
        }
    }
}