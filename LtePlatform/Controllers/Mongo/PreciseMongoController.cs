using System;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiGroup("����")]
    [ApiControl("��MongoDB���뾫ȷ������ָ�������")]
    public class PreciseMongoController : ApiController
    {
        private readonly PreciseImportService _service;

        public PreciseMongoController(PreciseImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("����ĳһ���ͳ�Ƽ�¼")]
        [ApiParameterDoc("statDate", "��������")]
        [ApiResponse("�ɹ���������")]
        public int Get(DateTime statDate)
        {
            return _service.UpdateItems(statDate);
        }
    }
}