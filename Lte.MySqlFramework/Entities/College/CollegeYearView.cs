using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.College;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.College
{
    [AutoMapFrom(typeof(CollegeYearInfo))]
    [TypeDoc("校园网年度信息")]
    public class CollegeYearView
    {
        [MemberDoc("校园网名称")]
        public string Name { get; set; }

        [MemberDoc("年份")]
        public int Year { get; set; }

        [MemberDoc("总学生数量")]
        public int TotalStudents { get; set; }

        [MemberDoc("当前电信用户数")]
        public int CurrentSubscribers { get; set; }

        [MemberDoc("毕业学生用户数")]
        public int GraduateStudents { get; set; }

        [MemberDoc("新用户数")]
        public int NewSubscribers { get; set; }

        [MemberDoc("老生开学日期")]
        public DateTime OldOpenDate { get; set; }

        [MemberDoc("新生开学日期")]
        public DateTime NewOpenDate { get; set; }

        [MemberDoc("用户数到达")]
        public int ExpectedSubscribers { get; set; }
    }
}