using System;
using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities
{
    public class UserLoginSessionInfo : Entity
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime ExpireTime { get; set; }

        public DateTime LogoffTime { get; set; }

        public string RoleList { get; set; }
    }

    public class ModuleUsageRecord : Entity
    {
        public int SessionId { get; set; }

        public DateTime UsingTime { get; set; }

        public string ModuleUrl { get; set; }
    }
}
