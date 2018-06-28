using LtePlatform.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace LtePlatform.Results
{
    public static class SignInManagerExtension
    {
        public async static Task CheatTwoFactorSingIn(this SignInManager<ApplicationUser, string> manager,
            bool isPersistent, bool rememberBrowser)
        {
            var userId = await manager.GetVerifiedUserIdAsync();
            var user = await manager.UserManager.FindByIdAsync(userId);
            await manager.UserManager.ResetAccessFailedCountAsync(user.Id);
            await manager.SignInAsync(user, isPersistent, rememberBrowser);
        }
    }
}
