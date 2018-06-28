using System.Threading.Tasks;
using System.Web.Http;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    [Authorize(Roles = "管理员")]
    [ApiControl("删除角色控制器")]
    public class DeleteRoleController : ApiController
    {
        [HttpGet]
        [ApiDoc("删除一个角色")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiResponse("操作结果信息")]
        public async Task<string> Get(string roleName)
        {
            var context = ApplicationDbContext.Create();
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            if (!roleManager.RoleExists(roleName)) return "删除角色失败，该角色名称不存在";
            var role = roleManager.FindByName(roleName);
            await roleManager.DeleteAsync(role);
            return "删除角色成功";
        } 
    }
}