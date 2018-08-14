using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(ComplainItem))]
    [TypeDoc("���Ͷ�߹���")]
    public class ComplainDto : IStateChange, IBeginDate, ICityDistrictTown, IGeoPoint<double>
    {
        [MemberDoc("�������")]
        public string SerialNumber { get; set; }

        [MemberDoc("�ͻ��绰")]
        public string SubscriberPhone { get; set; }

        [MemberDoc("�ظ�����")]
        public byte RepeatTimes { get; set; }

        [AutoMapPropertyResolve("ServiceCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        public string ServiceCategoryDescription { get; set; }

        [AutoMapPropertyResolve("IsUrgent", typeof(ComplainItem), typeof(YesNoTransform))]
        public string IsUrgentDescription { get; set; }

        [MemberDoc("�ͻ�����")]
        public string SubscriberInfo { get; set; }

        [MemberDoc("��ϵ�绰1")]
        public string ContactPhone { get; set; }

        [MemberDoc("��ϵ��")]
        public string ContactPerson { get; set; }

        [MemberDoc("��ϵ��ַ")]
        public string ContactAddress { get; set; }

        [MemberDoc("�����˰���")]
        public string ManagerInfo { get; set; }

        [MemberDoc("��������")]
        public string ComplainContents { get; set; }

        [MemberDoc("����ʱ��")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("ȫ�̳�ʱʱ��")]
        public DateTime Deadline { get; set; }

        [MemberDoc("��ǰ��������")]
        public string CurrentProcessor { get; set; }

        [MemberDoc("��ǰ����ӵ�ʱ��")]
        public DateTime ProcessTime { get; set; }

        [MemberDoc("������ά��ˮ��")]
        public string OssSerialNumber { get; set; }

        [AutoMapPropertyResolve("ComplainSource", typeof(ComplainItem), typeof(ComplainSourceDescriptionTransform))]
        [MemberDoc("������Դ")]
        public string ComplainSourceDescription { get; set; }

        public DateTime BeginTime { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        [MemberDoc("·��")]
        public string RoadName { get; set; }

        [MemberDoc("¥������")]
        public string BuildingName { get; set; }
        
        [MemberDoc("ԭ����")]
        public string CauseLocation { get; set; }

        [MemberDoc("Ԥ��������")]
        public string PreProcessContents { get; set; }

        [ExcelColumn("�ص�����")]
        public string FinishContents { get; set; }

        [AutoMapPropertyResolve("IsSubscriber4G", typeof(ComplainItem), typeof(YesNoTransform))]
        [MemberDoc("�Ƿ�Ϊ4G�û�")]
        public string IsSubscriber4GDescription { get; set; }

        [MemberDoc("����")]
        public double Longtitute { get; set; }

        [MemberDoc("γ��")]
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("ComplainReason", typeof(ComplainItem), typeof(ComplainReasonDescriptionTransform))]
        [MemberDoc("ԭ����һ��")]
        public string ComplainReasonDescription { get; set; }

        [AutoMapPropertyResolve("ComplainSubReason", typeof(ComplainItem), typeof(ComplainSubReasonDescriptionTransform))]
        [MemberDoc("ԭ���Զ���")]
        public string ComplainSubReasonDescription { get; set; }

        [MemberDoc("��������")]
        public string Grid { get; set; }

        [AutoMapPropertyResolve("NetworkType", typeof(ComplainItem), typeof(NetworkTypeDescriptionTransform))]
        [MemberDoc("ҵ������")]
        public string NetworkTypeDescription { get; set; }

        [MemberDoc("���վ������")]
        public string SitePosition { get; set; }

        [AutoMapPropertyResolve("IsIndoor", typeof(ComplainItem), typeof(IndoorDescriptionTransform))]
        [MemberDoc("��������")]
        public string IsIndoorDescription { get; set; }

        [AutoMapPropertyResolve("ComplainScene", typeof(ComplainItem), typeof(ComplainSceneDescriptionTransform))]
        [MemberDoc("ʹ�ó���")]
        public string ComplainSceneDescription { get; set; }

        [AutoMapPropertyResolve("ComplainCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        [MemberDoc("�������")]
        public string ComplainCategoryDescription { get; set; }

        [AutoMapPropertyResolve("ComplainState", typeof(ComplainItem), typeof(ComplainStateDescriptionTransform))]
        [MemberDoc("����״̬")]
        public string CurrentStateDescription { get; set; }

        public string NextStateDescription
        {
            get
            {
                var nextState = CurrentStateDescription.GetNextStateDescription(ComplainState.Archive);
                return nextState == null ? null : ((ComplainState)nextState).GetEnumDescription();
            }
        }

        [MemberDoc("�Ƿ�Ϊ�ٶȾ�γ��")]
        public bool IsBaiduOffset { get; set; }
    }
}