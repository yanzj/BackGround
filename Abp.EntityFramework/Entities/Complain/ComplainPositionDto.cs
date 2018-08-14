using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(ComplainItem))]
    [TypeDoc("��Թ��Ϣ�����Ͷ�߹������뾭γ��У�������Ϣ��ͼ")]
    public class ComplainPositionDto
    {
        [MemberDoc("Ͷ�ߵ����")]
        public string SerialNumber { get; set; }

        [MemberDoc("����")]
        public string City { get; set; }

        [MemberDoc("����")]
        public string District { get; set; }
        
        [MemberDoc("����")]
        public string Town { get; set; }

        [MemberDoc("��·���ƣ���Ϊƥ�����λ�õĵ�һ��Ҫ��Ϣ")]
        public string RoadName { get; set; }

        [MemberDoc("¥�����ƣ���Ϊƥ�����λ�õĵڶ���Ҫ��Ϣ")]
        public string BuildingName { get; set; }

        [MemberDoc("���ȣ���Ҫƥ��ľ��ȣ�ͨ���ٶȵ�ͼAPI��ȡ")]
        public double Longtitute { get; set; }

        [MemberDoc("γ�ȣ���Ҫƥ��ľ��ȣ�ͨ���ٶȵ�ͼAPI��ȡ")]
        public double Lattitute { get; set; }

        [MemberDoc("վ��λ�ã���Ϊƥ�����λ�õĵ�����Ҫ��Ϣ")]
        public string SitePosition { get; set; }

        [MemberDoc("Ͷ�����ݣ���Ϊƥ�����λ�õĴ���Ҫ��Ϣ")]
        public string ComplainContents { get; set; }

        [MemberDoc("��ϵ��ַ����Ϊƥ�����λ�õĴ�Ҫ��Ϣ����Ϊ�û�������ַͨ����Ͷ�ߵ�ַ����ͬһ��ַ")]
        public string ContactAddress { get; set; }
    }
}