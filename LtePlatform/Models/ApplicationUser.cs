using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;
using System;

namespace LtePlatform.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据，若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public string Hometown { get; set; }

        public int LoginTimes { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        { }

        public ApplicationRole(string name) : base(name) { }
    }

    [TypeDoc("角色分配用户定义")]
    public class RoleUsersDto
    {
        [MemberDoc("角色名称")]
        public string RoleName { get; set; }

        [MemberDoc("用户名称列表")]
        public IEnumerable<string> UserNames { get; set; }
    }
}