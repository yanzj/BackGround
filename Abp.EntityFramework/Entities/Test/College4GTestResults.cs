using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Test
{
    [TypeDoc("校园网4G测试结果")]
    [AutoMapFrom(typeof(College4GTestView))]
    public class College4GTestResults : Entity
    {
        [MemberDoc("校园编号")]
        public int CollegeId { get; set; }

        [MemberDoc("测试时间")]
        public DateTime TestTime { get; set; }

        public string Place { get; set; }

        public string Tester { get; set; }

        [MemberDoc("下载速率")]
        public double DownloadRate { get; set; }

        [MemberDoc("上传速率")]
        public double UploadRate { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("接入用户数")]
        public int AccessUsers { get; set; }

        [MemberDoc("RSRP")]
        public double Rsrp { get; set; }

        [MemberDoc("SINR")]
        public double Sinr { get; set; }
    }
}