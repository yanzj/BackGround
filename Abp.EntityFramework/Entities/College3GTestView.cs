using System;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(College3GTestResults))]
    public class College3GTestView
    {
        public DateTime TestTime { get; set; }

        public string CollegeName { get; set; }

        public string Place { get; set; }

        public string Tester { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }
    }
}