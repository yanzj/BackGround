using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [TypeDoc("定义LTE基站数据库表对应的ORM对象")]
    [AutoMapFrom(typeof(ENodebExcel), typeof(ENodebBaseExcel))]
    public class ENodeb : Entity, IGeoPoint<double>, IIsInUse, ITownId
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MaxLength(50)]
        [MemberDoc("基站名称")]
        [AutoMapPropertyResolve("ENodebName", typeof(ENodebBaseExcel))]
        public string Name { get; set; }

        [MemberDoc("镇区编号")]
        public int TownId { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactoryDescription", typeof(ENodebBaseExcel))]
        public string Factory { get; set; }

        [MemberDoc("FDD制式")]
        [AutoMapPropertyResolve("DivisionDuplex", typeof(ENodebExcel), typeof(FddTransform))]
        public bool IsFdd { get; set; } = true;

        [MemberDoc("地址")]
        public string Address { get; set; }

        [MemberDoc("网关")]
        [AutoMapPropertyResolve("GatewayIp", typeof(ENodebExcel), typeof(IpAddressTransform))]
        [AutoMapPropertyResolve("GatewayIpAddress", typeof(ENodebBaseExcel), typeof(IpAddressTransform))]
        public int Gateway { get; set; }

        [MemberDoc("子IP")]
        [AutoMapPropertyResolve("Ip", typeof(ENodebExcel), typeof(IpByte4Transform))]
        [AutoMapPropertyResolve("Ip", typeof(ENodebBaseExcel), typeof(IpByte4Transform))]
        public byte SubIp { get; set; }

        [MemberDoc("网关IP")]
        public IpAddress GatewayIp => new IpAddress { AddressValue = Gateway };

        [MemberDoc("IP")]
        public IpAddress Ip => new IpAddress { AddressValue = Gateway, IpByte4 = SubIp };

        [MemberDoc("规划编号(设计院)")]
        public string PlanNum { get; set; }

        [MemberDoc("入网日期")]
        [AutoMapPropertyResolve("FinishTime", typeof(ENodebBaseExcel))]
        public DateTime OpenDate { get; set; }

        [MemberDoc("是否在用")]
        public bool IsInUse { get; set; } = true;

    }
}