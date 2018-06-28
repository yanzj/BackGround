using System.Threading.Tasks;
using System.Web.Http;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    [Authorize(Roles = "管理员")]
    [ApiControl("新增角色控制器")]
    public class CreateRoleController : ApiController
    {
        [HttpGet]
        [ApiDoc("新增一个角色")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiResponse("操作结果信息")]
        public async Task<string> Get(string roleName)
        {
            var context = ApplicationDbContext.Create();
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            if (roleManager.RoleExists(roleName)) return "新增角色失败，该角色名称已存在";
            await roleManager.CreateAsync(new ApplicationRole(roleName));
            return "新增角色成功";
        } 
    }
}