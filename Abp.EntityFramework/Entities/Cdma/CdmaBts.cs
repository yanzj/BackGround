using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Cdma
{
    [TypeDoc("定义CDMA基站的数据库对应的ORM对象")]
    [AutoMapFrom(typeof(BtsExcel))]
    public class CdmaBts : Entity, IGeoPoint<double>, ITownId
    {
        public int ENodebId { get; set; } = -1;

        [MaxLength(50)]
        public string Name { get; set; }

        public int TownId { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string Address { get; set; }

        public int BtsId { get; set; }

        public short BscId { get; set; }

        public bool IsInUse { get; set; } = true;
    }

    [AutoMapFrom(typeof(CdmaBts))]
    [TypeDoc("CDMA基站视图")]
    public class CdmaBtsView : IGeoPoint<double>
    {
        [MemberDoc("基站名称")]
        public string Name { get; set; }

        [MemberDoc("所属镇区编号")]
        public int TownId { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        public string CityName { get; set; }

        [MemberDoc("区域")]
        public string DistrictName { get; set; }

        [MemberDoc("镇区")]
        public string TownName { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("地址")]
        public string Address { get; set; }

        [MemberDoc("基站编号")]
        public int BtsId { get; set; }

        [MemberDoc("BSC编号")]
        public int BscId { get; set; }

        [MemberDoc("是否在用")]
        public bool IsInUse { get; set; }
    }

    public class CdmaBtsCluster
    {
        public int LongtituteGrid { get; set; }

        public double Longtitute => (double) LongtituteGrid / 100000;

        public int LattituteGrid { get; set; }

        public double Lattitute => (double) LattituteGrid / 100000;

        public IEnumerable<CdmaBtsView> CdmaBtsViews { get; set; }
    }

}