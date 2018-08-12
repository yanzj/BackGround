using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(CollegeYearInfo))]
    public class CollegeYearView
    {
        public string Name { get; set; }

        public int TotalStudents { get; set; }

        public int CurrentSubscribers { get; set; }

        public int GraduateStudents { get; set; }

        public int NewSubscribers { get; set; }

        public DateTime OldOpenDate { get; set; }

        public DateTime NewOpenDate { get; set; }

        public int ExpectedSubscribers { get; set; }
    }
}