using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Account
{
    [ApiControl("用户登录访问记录查询控制器")]
    public class UserRecordController : ApiController
    {
        private readonly UserRecordService _recordService;

        public UserRecordController(UserRecordService recordService)
        {

            _recordService = recordService;
        }

        [HttpGet]
        public void Get(string userName, string token, string roles)
        {

            _recordService.CreateOneSession(userName, token, roles);
        }

        [HttpGet]
        [ApiDoc("获取当前会话对应的权限列表，如果会话过期，则返回空列表")]
        [ApiParameterDoc("userName", "用户名")]
        [ApiParameterDoc("token", "会话令牌")]
        [ApiResponse("当前会话对应的权限列表")]
        public string[] Get(string userName, string token)
        {
            return _recordService.QueryCurrentSessionRoles(userName, token);
        }

        [HttpGet]
        [ApiDoc("退出当前会话")]
        [ApiParameterDoc("logoffName", "用户名")]
        [ApiParameterDoc("logoffToken", "会话令牌")]
        public bool GetLogoff(string logoffName, string logoffToken)
        {
            return _recordService.LogoffCurrentSession(logoffName, logoffToken);
        }
    }
}
