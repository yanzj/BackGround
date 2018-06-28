using System;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;

namespace LtePlatform.Models
{
    public class UserRecordService
    {
        private readonly IUserLoginSessionInfoRepository _sessionInfoRepository;
        private readonly IModuleUsageRecordRepository _usageRecordRepository;

        public UserRecordService(IUserLoginSessionInfoRepository sessionInfoRepository,
            IModuleUsageRecordRepository usageRecordRepository)
        {
            _sessionInfoRepository = sessionInfoRepository;
            _usageRecordRepository = usageRecordRepository;
        }

        public void CreateOneSession(string userName, string token, string roles)
        {
            _sessionInfoRepository.Insert(new UserLoginSessionInfo
            {
                UserName = userName,
                Token = token,
                LoginTime = DateTime.Now,
                LogoffTime = DateTime.Now.AddDays(2),
                ExpireTime = DateTime.Now.AddHours(36),
                RoleList = roles
            });
            _sessionInfoRepository.SaveChanges();
        }

        public string[] QueryCurrentSessionRoles(string userName, string token)
        {
            var session = _sessionInfoRepository.FirstOrDefault(x => x.UserName == userName && x.Token == token);
            if (session == null) return new string[] {};
            return session.ExpireTime < DateTime.Now ? new string[] { } : session.RoleList.Split(',');
        }

        public bool LogoffCurrentSession(string userName, string token)
        {
            var session = _sessionInfoRepository.FirstOrDefault(x => x.UserName == userName && x.Token == token);
            if (session == null) return false;
            session.LogoffTime = DateTime.Now;
            _sessionInfoRepository.SaveChanges();
            return true;
        }
    }
}