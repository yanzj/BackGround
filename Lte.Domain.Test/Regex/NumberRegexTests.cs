using Lte.Domain.Regular;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Test.Regex
{
    [TestFixture]
    public class NumberRegexTests
    {
        [TestCase("1234", 1)]
        [TestCase("12.34", 2)]
        [TestCase("12x34y55", 3)]
        [TestCase("eNodeB=502990", 1)]
        public void TestGetAllNumber(string source, int count)
        {
            var results = RegexService.GetAllNumberByString(source);
            Assert.AreEqual(results.Count, count);
        }

        [TestCase("eNodeB=502990", 502990)]
        [TestCase(@"【告警信息】: 网元断链告警<br/>【告警位置】: /LteSystem=佛山/Voder=2/eNodeB=502990<br/>【告警时间】: 2018-06-15 23:28:02<br/>【告警级别】: C<br/>【告警详细信息】: sdrmgr:OMMOID=ibxbcj0r-1@sbn=440609@NodeMe=502990   IP: 8.142.5.46; 检测时间点: 2018-06-15 23:27:11, 网管至网元OMC通道引用的IP层配置的网关(IP: 8.142.5.1)传输正常; L eNBId:502990<br/>【告警来源】: 省网管L网中兴<br/>【网格信息】: 网格名称:FS禅城<br/>【基站等级】: C", 502990)]
        [TestCase(@"【告警信息】: 小区不可用告警<br/>【告警位置】: /LteSystem=佛山/Voder=1/eNodeB=552451/RRU=NB勒流富华集团R_5<br/>【告警时间】: 2018-06-15 18:20:03<br/>【告警级别】: D<br/>【告警详细信息】: eNodeB名称=勒流江村接入机房LBBU6, 本地小区标识=11, 小区双工模式=FDD, NB-IoT小区指示=TRUE, 小区名称=NB勒流富华集团R_5, eNodeB标识=552451, 小区标识=82, 具体问题=射频单元异常   基站制式=L, 影响制式=L, 部署标识=NULL, 累计时长(s)=90, eNodeBId=552451<br/>【告警来源】: 省网管L网华为<br/>【网格信息】: 网格名称:FS顺德<br/>【基站等级】: C
<br/>【压缩信息】: 告警对象友好名称：NB勒流富华集团R_5,网元名称：勒流江村接入机房LBBU6
<br/>告警对象友好名称：勒流富华集团R_5,网元名称：勒流江村接入机房LBBU6
<br/>告警对象友好名称：大良国泰路R_2,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：大良国泰路R_1,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：大良国泰路R_0,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：容桂振华营业厅（第二载波）,网元名称：振华机楼LBBU4
<br/>告警对象友好名称：容桂振华营业厅,网元名称：振华机楼LBBU4", 552451)]
        [TestCase(@"【告警信息】: LTE小区退出服务<br/>【告警位置】: /LteSystem=佛山/Voder=2/eNodeB=551676/RRU=南庄电信LBBU32_1<br/>【告警时间】: 2018-06-15 19:56:01<br/>【告警级别】: D<br/>【告警详细信息】: sdrmgr:OMMOID=ibxbcj0r-1@sbn=440609@NodeMe=551676,Equipment=1,rack=1,shelf=1,board=1   小区用户标识: 南庄电信LBBU32_1, 小区标识: 1, RRU板异常导致; 小区类型：普通小区; 单板序列号: 707124500976; L eNBId:551676<br/>【告警来源】: 省网管L网中兴<br/>【网格信息】: 网格名称:FS禅城<br/>【基站等级】: C
< br />【压缩信息】: 告警对象友好名称：南庄电信LBBU32_1, 网元名称：南庄电信LBBU32", 551676)]
        public void TestGetENodebIdByString(string source, int eNodebId)
        {
            var result = source.GetENodebIdByString();
            Assert.AreEqual(result, eNodebId);
        }

        [TestCase(@"【告警信息】: 网元断链告警<br/>【告警位置】: /LteSystem=佛山/Voder=2/eNodeB=502990<br/>【告警时间】: 2018-06-15 23:28:02<br/>【告警级别】: C<br/>【告警详细信息】: sdrmgr:OMMOID=ibxbcj0r-1@sbn=440609@NodeMe=502990   IP: 8.142.5.46; 检测时间点: 2018-06-15 23:27:11, 网管至网元OMC通道引用的IP层配置的网关(IP: 8.142.5.1)传输正常; L eNBId:502990<br/>【告警来源】: 省网管L网中兴<br/>【网格信息】: 网格名称:FS禅城<br/>【基站等级】: C", 255)]
        [TestCase(@"【告警信息】: 小区不可用告警<br/>【告警位置】: /LteSystem=佛山/Voder=1/eNodeB=552451/RRU=NB勒流富华集团R_5<br/>【告警时间】: 2018-06-15 18:20:03<br/>【告警级别】: D<br/>【告警详细信息】: eNodeB名称=勒流江村接入机房LBBU6, 本地小区标识=11, 小区双工模式=FDD, NB-IoT小区指示=TRUE, 小区名称=NB勒流富华集团R_5, eNodeB标识=552451, 小区标识=82, 具体问题=射频单元异常   基站制式=L, 影响制式=L, 部署标识=NULL, 累计时长(s)=90, eNodeBId=552451<br/>【告警来源】: 省网管L网华为<br/>【网格信息】: 网格名称:FS顺德<br/>【基站等级】: C
<br/>【压缩信息】: 告警对象友好名称：NB勒流富华集团R_5,网元名称：勒流江村接入机房LBBU6
<br/>告警对象友好名称：勒流富华集团R_5,网元名称：勒流江村接入机房LBBU6
<br/>告警对象友好名称：大良国泰路R_2,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：大良国泰路R_1,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：大良国泰路R_0,网元名称：德胜机楼二LBBU28
<br/>告警对象友好名称：容桂振华营业厅（第二载波）,网元名称：振华机楼LBBU4
<br/>告警对象友好名称：容桂振华营业厅,网元名称：振华机楼LBBU4", 82)]
        [TestCase(@"【告警信息】: LTE小区退出服务<br/>【告警位置】: /LteSystem=佛山/Voder=2/eNodeB=551676/RRU=南庄电信LBBU32_1<br/>【告警时间】: 2018-06-15 19:56:01<br/>【告警级别】: D<br/>【告警详细信息】: sdrmgr:OMMOID=ibxbcj0r-1@sbn=440609@NodeMe=551676,Equipment=1,rack=1,shelf=1,board=1   小区用户标识: 南庄电信LBBU32_1, 小区标识: 1, RRU板异常导致; 小区类型：普通小区; 单板序列号: 707124500976; L eNBId:551676<br/>【告警来源】: 省网管L网中兴<br/>【网格信息】: 网格名称:FS禅城<br/>【基站等级】: C
< br />【压缩信息】: 告警对象友好名称：南庄电信LBBU32_1, 网元名称：南庄电信LBBU32", 1)]
        [TestCase(@"【告警信息】: LTE小区退出服务<br/>【告警位置】: /LteSystem=佛山/Voder=2/eNodeB=551080/RRU=金沙联沙接入机房LBBU2_2<br/>【告警时间】: 2018-06-16 00:45:24<br/>【告警级别】: D<br/>【告警详细信息】: sdrmgr:OMMOID=ibxbdx45-3@sbn=440610@NodeMe=551080,Equipment=1,rack=1,shelf=1,board=1   小区用户标识: 金沙联沙接入机房LBBU2_2, 小区标识: 50, RRU板异常导致; 小区类型：普通小区; 单板序列号: 282789200801; L eNBId:551080<br/>【告警来源】: 省网管L网中兴<br/>【网格信息】: 网格名称:FS南海<br/>【基站等级】: B", 50)]
        public void TestGetSectorIdByString(string source, byte sectorId)
        {
            var result = source.GetSectorIdByString();
            Assert.AreEqual(result, sectorId);
        }

        [TestCase("12345")]
        [TestCase("-1.78")]
        [TestCase("Abcd88x")]
        [TestCase("#*wqio90")]
        public void TestCheckNumber_True(string source)
        {
            Assert.IsTrue(RegexService.CheckNumberByString(source));
        }

        [TestCase("Abb")]
        [TestCase("-%^&^*")]
        [TestCase("^%UYnk")]
        public void Test_CheckNumber_False(string source)
        {
            Assert.IsFalse(RegexService.CheckNumberByString(source));
        }

    }
}
