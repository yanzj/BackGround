using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Station
{
    [AutoMapFrom(typeof(IndoorDistributionExcel))]
    [TypeDoc("��������ƽ̨�ҷ���Ϣ")]
    public class IndoorDistribution : Entity, IGeoPoint<double>
    {
        [MemberDoc("�ҷֱ���")]
        public string IndoorSerialNum { get; set; }

        [MemberDoc("�ҷֵ�ַ")]
        public string Address { get; set; }

        [MemberDoc("��/��/��/��")]
        public string StationDistrict { get; set; }

        [MemberDoc("��/��/�ֵ�")]
        public string StationTown { get; set; }

        [MemberDoc("����")]
        public double Longtitute { get; set; }

        [MemberDoc("γ��")]
        public double Lattitute { get; set; }

        [MemberDoc("С����ʶ")]
        public string CellSerialNum { get; set; }

        [MemberDoc("RRU��ʶ")]
        public string RruSerialNum { get; set; }

        [MemberDoc("�ҷ�����")]
        [AutoMapPropertyResolve("IndoorCategoryDescription", typeof(IndoorDistributionExcel), typeof(IndoorCategoryTransform))]
        public IndoorCategory IndoorCategory { get; set; }

        [MemberDoc("��������")]
        public string CoverageArea { get; set; }

        [MemberDoc("������")]
        public string Integritor { get; set; }

        [MemberDoc("�ֲ�ϵͳ����")]
        [AutoMapPropertyResolve("IndoorNetworkDescription", typeof(IndoorDistributionExcel), typeof(IndoorNetworkTransform))]
        public IndoorNetwork IndoorNetwork { get; set; }

        [MemberDoc("�Ƿ��·")]
        [AutoMapPropertyResolve("HasCombiner", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsHasCombiner { get; set; }

        [MemberDoc("��·��ʽ")]
        [AutoMapPropertyResolve("CombinerFunctionDescription", typeof(IndoorDistributionExcel), typeof(CombinerFunctionTransform))]
        public CombinerFunction CombinerFunction { get; set; }

        [MemberDoc("LTE�Ƿ��·�Ͼ��ҷ�")]
        [AutoMapPropertyResolve("OldCombinerDescription", typeof(IndoorDistributionExcel), typeof(OldCombinerTransform))]
        public OldCombiner OldCombiner { get; set; }

        [MemberDoc("L����Դ����")]
        public byte LteSources { get; set; }

        [MemberDoc("��·������")]
        public string CombinerIntegrator { get; set; }

        [MemberDoc("����")]
        [AutoMapPropertyResolve("DistributionClassDescription", typeof(IndoorDistributionExcel), typeof(ENodebClassTransform))]
        public ENodebClass DistributionClass { get; set; }

        [MemberDoc("Ѳ����ϸλ��")]
        public string CheckingAddress { get; set; }

        [MemberDoc("�Ƿ���������Ӫ�̺�·")]
        [AutoMapPropertyResolve("CombinedWithOtherOperator", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsCombinedWithOtherOperator { get; set; }

        [MemberDoc("L����ͨʱ��")]
        public DateTime? LteOpenDate { get; set; }

        [MemberDoc("����˫ͨ��")]
        [AutoMapPropertyResolve("DistributionChannelDescription", typeof(IndoorDistributionExcel), typeof(DistributionChannelTransform))]
        public DistributionChannel DistributionChannel { get; set; }

        [MemberDoc("¥������")]
        public string BuildingName { get; set; }

        [MemberDoc("¥�����")]
        public string BuildingCode { get; set; }

        [MemberDoc("¥���ַ")]
        public string BuildingAddress { get; set; }

        [MemberDoc("������")]
        public byte? TotalLifts { get; set; }

        [MemberDoc("���")]
        public double? BuildingArea { get; set; }

        [MemberDoc("����")]
        public int TotalFloors { get; set; }

        [MemberDoc("¥���Ƿ��е���ͣ����")]
        [AutoMapPropertyResolve("HasUnderGroundParker", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsHasUnderGroundParker { get; set; }

        [MemberDoc("�������")]
        public double? CoverageBuildingArea { get; set; }

        [MemberDoc("����¥����")]
        public int? CoverageFloors { get; set; }

        [MemberDoc("�Ƿ�ʹ����ż���㸲��")]
        [AutoMapPropertyResolve("EvenOddFloorCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsEvenOddFloorCoverage { get; set; }

        [MemberDoc("LTE�ҷ��Ƿ�ȫ����")]
        [AutoMapPropertyResolve("LteFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsLteFullCoverage { get; set; }

        [MemberDoc("�����Ƿ�LTE�ҷָ���")]
        [AutoMapPropertyResolve("LiftLteFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsLiftLteFullCoverage { get; set; }

        [MemberDoc("�������Ƿ�LTE�ҷָ���")]
        [AutoMapPropertyResolve("UndergroundFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsUndergroundFullCoverage { get; set; }

        [MemberDoc("����δ������������")]
        public string OtherNoCoverageComments { get; set; }

        [MemberDoc("ͣ�����������")]
        public string ParkCoverages { get; set; }

        [MemberDoc("ҵ��")]
        public string Yezhu { get; set; }

        [MemberDoc("�绰")]
        public string YezhuPhone { get; set; }

        [MemberDoc("��������ļ�")]
        public string FirstDesignFile { get; set; }

        [MemberDoc("���������ļ�")]
        public string FirstYanshouFile { get; set; }

        [MemberDoc("��������ļ�")]
        public string ModifyDesignFile { get; set; }

        [MemberDoc("����������")]
        public string ModifyYanshouFile { get; set; }

        [MemberDoc("ά����")]
        public string Maintainor { get; set; }

        [MemberDoc("ά������ϵ��ʽ")]
        public string MaintainContact { get; set; }

        [MemberDoc("��ע")]
        public string Comments { get; set; }

        [MemberDoc("�����˹�����ʱ��")]
        public DateTime? DataUpdateTime { get; set; }

        [MemberDoc("���ݸ�����")]
        public string DataUpdater { get; set; }

        [MemberDoc("�Զ���1")]
        public string Customize1 { get; set; }

        [MemberDoc("�Զ���2")]
        public string Customize2 { get; set; }

        [MemberDoc("�Զ���3")]
        public string Customize3 { get; set; }
    }
}