using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团站址信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationInfoUpdateController : ApiController
    {
        private readonly StationInfoService _service;

        public StationInfoUpdateController(StationInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新站址位置信息，包括经纬度和地址")]
        [ApiParameterDoc("serialNum", "需要更新的站址编码")]
        [ApiParameterDoc("longtitute", "经度")]
        [ApiParameterDoc("lattitute", "纬度")]
        [ApiParameterDoc("address", "地址")]
        [ApiResponse("更新是否成功")]
        public bool UpdatePositionInfo(string serialNum, double longtitute, double lattitute, string address)
        {
            return _service.UpdateStationPosition(serialNum, longtitute, lattitute, address);
        }

        [HttpGet]
        [ApiDoc("更新站址BBU基本信息，包括BBU堆叠方式和引电方式等")]
        [ApiParameterDoc("serialNum", "需要更新的站址编码")]
        [ApiParameterDoc("bbuHeapStation", "是否BBU堆叠站址，用于表示BBU池和多BBU共址的情况，取值范围是或否")]
        [ApiParameterDoc("cAndLCoExist", "是否C和L共站，用于表示C/L共站的情况，取值范围是或否")]
        [ApiParameterDoc("rruBufang", "是否布放RRU设备，用于表示站内是否放置了RRU设备，取值范围是或否")]
        [ApiParameterDoc("electricFunctionDescription", "引电方式，供电引电/业主引电")]
        [ApiParameterDoc("electricTypeDescription", "引电类型，高压引电/低压引电")]
        [ApiParameterDoc("batteryTypeDescription", "蓄电池类型，铅酸电池/铁铝电池")]
        [ApiResponse("更新是否成功")]
        public bool UpdateBbuBasicInfo(string serialNum, string bbuHeapStation, string cAndLCoExist,
            string rruBufang, string electricFunctionDescription, string electricTypeDescription,
            string batteryTypeDescription)
        {
            return _service.UpdateStationBbuBasicInfo(serialNum, bbuHeapStation, cAndLCoExist, rruBufang,
                electricFunctionDescription, electricTypeDescription, batteryTypeDescription);
        }

        [HttpGet]
        [ApiDoc("更新站址BBU机房和塔桅信息")]
        [ApiParameterDoc("serialNum", "需要更新的站址编码")]
        [ApiParameterDoc("operatorUsageDescription", "站址机房共用情况，电信独有|电信移动|电信联通|电信移动联通|无机房|其他")]
        [ApiParameterDoc("stationRoomBelongDescription", "站址机房产权方，电信|移动|联通|铁塔公司|无机房|其他")]
        [ApiParameterDoc("stationTowerBelongDescription", "站址塔桅产权方，电信|移动|联通|铁塔公司|无机房|其他")]
        [ApiParameterDoc("stationSupplyBelongDescription", "站址发电责任方，电信|移动|联通|铁塔公司|无机房|其他")]
        [ApiParameterDoc("towerTypeDescription", "塔桅类型，无塔桅|落地角钢塔|落地景观塔|落地拉线塔|落地单管塔|落地三管塔|落地四管塔|楼顶抱杆|楼顶角钢塔|楼顶美化天线|楼顶拉线塔|楼顶组合抱杆|楼顶景观塔|H杆|水泥杆|路灯杆|监控立杆|落地四角塔|楼顶单管塔|其他")]
        [ApiParameterDoc("totalPlatforms", "平台总数(个)，对楼顶抱杆情况，平台数量为1，无塔桅的情况，填写0")]
        [ApiParameterDoc("towerHeight", "塔桅高度(米)，不包含楼面的高度，无塔桅的情况，填写0")]
        [ApiResponse("更新是否成功")]
        public bool UpdateRoomAndTowerInfo(string serialNum, string operatorUsageDescription,
            string stationRoomBelongDescription, string stationTowerBelongDescription,
            string stationSupplyBelongDescription, string towerTypeDescription, int totalPlatforms, double towerHeight)
        {
            return _service.UpdateStationRoomAndTowerInfo(serialNum, operatorUsageDescription,
                stationRoomBelongDescription,
                stationTowerBelongDescription, stationSupplyBelongDescription, towerTypeDescription, totalPlatforms,
                towerHeight);
        }
    }
}