using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lte.Domain.Regular.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LtePlatform.Models
{
    [TypeDoc("角色视图模版")]
    public class ApplicationRoleViewModel
    {
        [MemberDoc("角色名称")]
        public string Name { get; set; }

        [MemberDoc("角色编号")]
        public string RoleId { get; set; }
    }

    [TypeDoc("应用程序注册用户信息视图")]
    public class ApplicationUserViewModel
    {
        [MemberDoc("用户名")]
        public string UserName { get; set; }

        [MemberDoc("电话号码")]
        public string PhoneNumber { get; set; }

        [MemberDoc("家乡")]
        public string Hometown { get; set; }

        [MemberDoc("电子邮箱")]
        public string Email { get; set; }

        [MemberDoc("电子邮箱是否已确认")]
        public bool EmailHasBeenConfirmed { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "家乡")]
        public string Hometown { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class LoginExternalViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string PeerUrl { get; set; }
    }

    public class AuthorizationRequest
    {
        public string PeerUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RequestUrl { get; set; }

        public string ReturnType { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public string Email { get; set; }

        public bool EmailHasBeenConfirmed { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        public string Code { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "家乡")]
        public string Hometown { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}
