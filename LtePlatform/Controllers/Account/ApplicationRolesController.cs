using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LtePlatform.Controllers.Account
{
    [ApiControl("用户角色管理控制器")]
    [Authorize(Roles = "管理员")]
    public class ApplicationRolesController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public ApplicationRolesController()
        {
            var context = ApplicationDbContext.Create();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
        }

        [HttpGet]
        [ApiDoc("获取所有角色定义视图")]
        [ApiResponse("所有角色定义视图")]
        public IEnumerable<ApplicationRoleViewModel> Get()
        { 
            return _roleManager.Roles.Select(x=>new ApplicationRoleViewModel
            {
                Name = x.Name,
                RoleId = x.Id
            });
        }

        [HttpGet]
        [ApiDoc("分配角色和用户之间的绑定关系")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiParameterDoc("userName", "用户名称")]
        [ApiResponse("分配是否成功")]
        public bool Get(string roleName, string userName)
        {
            if (!_roleManager.RoleExists(roleName)) return false;
            var user = _userManager.FindByName(userName);
            if (user == null) return false;
            if (_userManager.IsInRole(user.Id, roleName)) return false;
            _userManager.AddToRole(user.Id, roleName);
            return true;
        }

        [HttpPost]
        [ApiDoc("批量向用户添加某个角色")]
        [ApiParameterDoc("dto", "角色添加信息，包含角色名称和待添加的用户列表")]
        [ApiResponse("添加是否成功")]
        public bool Post(RoleUsersDto dto)
        {
            if (!_roleManager.RoleExists(dto.RoleName)) return false;
            foreach (
                var user in
                    dto.UserNames.Select(userName => _userManager.FindByName(userName))
                        .Where(user => user != null)
                        .Where(user => !_userManager.IsInRole(user.Id, dto.RoleName)))
            {
                _userManager.AddToRole(user.Id, dto.RoleName);
            }
            return true;
        }

        [HttpGet]
        [ApiDoc("查询指定角色不具有的用户列表，以便增加角色")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiResponse("指定角色不具有的用户列表")]
        public IEnumerable<ApplicationUser> Get(string roleName)
        {
            if (!_roleManager.RoleExists(roleName)) return null;
            return _userManager.Users.Where(user => !_userManager.IsInRole(user.Id, roleName));
        }
    }
}
