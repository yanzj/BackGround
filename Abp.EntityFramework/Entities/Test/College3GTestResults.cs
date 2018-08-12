using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities.Test
{
    [AutoMapFrom(typeof(College3GTestView))]
    public class College3GTestResults : Entity
    {
        public int CollegeId { get; set; }

        public DateTime TestTime { get; set; }

        public string Place { get; set; }

        public string Tester { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }
    }
}