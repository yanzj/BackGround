using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Regular;
using Lte.Domain.Common.Geo;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class DtGeoPointTest
    {
        private const string ConnectionString = "Data source=219.128.254.41;initial catalog=masterTest;user id=ouyanghui;password=123456;";
        private string _excelFileName;
        private string _resultFileName;
        private string _mergeFileName;
        private string _lastFileName;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _excelFileName = Path.Combine(excelFilesDirectory, "GeoCombined.txt");
            _resultFileName = Path.Combine(excelFilesDirectory, "三网数据汇总.txt");
            _mergeFileName = Path.Combine(excelFilesDirectory, "MergeGeoPoints.txt");
            _lastFileName = Path.Combine(excelFilesDirectory, "LastGeoPoints.txt");
        }

        [TestCase("禅城_201701_1")]
        public void Test_ReadGeoPoints_FromOneTable(string tableName)
        {
            var helper = new DbHelper(ConnectionString);
            var selectTest = $"select * from {tableName}";
            var resultTable = helper.GetDataTable(selectTest);
            for (var i = 0; i < resultTable.Rows.Count; i++)
            {
                var lon = Convert.ToDouble(resultTable.Rows[i][3]);
                var lat = Convert.ToDouble(resultTable.Rows[i][4]);
                Console.WriteLine(lon+","+lat);
            }
        }

        [Test]
        public void Test_WriteToFile()
        {
            var writer = new StreamWriter(_excelFileName);
            for (int index = 1; index <= 18; index++)
            {
                var tableName = "禅城_201701_" + index;
                var helper = new DbHelper(ConnectionString);
                var selectTest = $"select * from {tableName}";
                var resultTable = helper.GetDataTable(selectTest);
                for (var i = 0; i < resultTable.Rows.Count; i++)
                {
                    var lon = Convert.ToDouble(resultTable.Rows[i][3]);
                    var lat = Convert.ToDouble(resultTable.Rows[i][4]);
                    writer.WriteLine(lon + "," + lat);
                }
            }
        }

        [Test]
        public void Test_MergePoints()
        {
            var points = new List<GeoPoint>();
            using (var reader = new StreamReader(_excelFileName))
            {
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var fields = line.Split(',');
                    var longtitute = fields[0].ConvertToDouble(0);
                    var lattitute = fields[1].ConvertToDouble(0);
                    points.Add(new GeoPoint(longtitute, lattitute));
                }
            }
            var mergePoints = from point in points
                group point by new
                {
                    X = (int)(point.Longtitute/0.0004),
                    Y = (int)(point.Lattitute/0.0004)
                }
                into g
                select new GeoPoint(g.Average(x => x.Longtitute), g.Average(x => x.Lattitute));
            using (var writer = new StreamWriter(_mergeFileName))
            {
                foreach (var mergePoint in mergePoints)
                {
                    writer.WriteLine(mergePoint.Longtitute + "," + mergePoint.Lattitute);
                }
            }
        }

        [Test]
        public void Test_ProcessPoints()
        {
            var points = new List<GeoPoint>();
            using (var reader = new StreamReader(_mergeFileName))
            {
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var fields = line.Split(',');
                    var longtitute = fields[0].ConvertToDouble(0);
                    var lattitute = fields[1].ConvertToDouble(0);
                    points.Add(new GeoPoint(longtitute, lattitute));
                }
            }
            var dtPoints = new List<AgisDtPoint>();
            using (var reader = new StreamReader(_resultFileName))
            {
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var fields = line.Split(',');
                    var dtPoint = new AgisDtPoint
                    {
                        Operator = fields[0],
                        Longtitute = fields[1].ConvertToDouble(0),
                        Lattitute = fields[2].ConvertToDouble(0),
                        UnicomRsrp = fields[3].ConvertToInt(-140),
                        MobileRsrp = fields[4].ConvertToInt(-140),
                        TelecomRsrp = fields[5].ConvertToInt(-140)
                    };
                    dtPoints.Add(dtPoint);
                }
            }
            using (var writer = new StreamWriter(_lastFileName))
            {
                foreach (var point in points)
                {
                    var mergePoints =
                        dtPoints.Where(
                            x =>
                                x.Longtitute > point.Longtitute - 0.0002 && x.Longtitute < point.Longtitute + 0.0002 &&
                                x.Lattitute > point.Lattitute - 0.0002 && x.Lattitute < point.Lattitute + 0.0002);
                    if (mergePoints.Any())
                    {
                        writer.WriteLine(mergePoints.First().Operator + "," + point.Longtitute + "," + point.Lattitute +
                                         "," + mergePoints.Average(x => x.UnicomRsrp) + "," +
                                         mergePoints.Average(x => x.MobileRsrp) + "," +
                                         mergePoints.Average(x => x.TelecomRsrp));
                    }
                    
                }
            }
        }

        [Test]
        public void Test_RectanglePoints()
        {
            using (var reader = new StreamReader(_resultFileName))
            {
                using (var writer = new StreamWriter(_lastFileName))
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        var fields = line.Split(',');
                        var dtPoint = new AgisDtPoint
                        {
                            Operator = "小范围",
                            Longtitute = fields[1].ConvertToDouble(0),
                            Lattitute = fields[2].ConvertToDouble(0),
                            UnicomRsrp = fields[3].ConvertToInt(-140),
                            MobileRsrp = fields[4].ConvertToInt(-140),
                            TelecomRsrp = fields[5].ConvertToInt(-140)
                        };
                        if (dtPoint.Longtitute > 113.08 && dtPoint.Longtitute < 113.11 && dtPoint.Lattitute > 23 &&
                            dtPoint.Lattitute < 23.02)
                        {
                            writer.WriteLine(dtPoint.Operator + "," + dtPoint.Longtitute + "," + dtPoint.Lattitute +
                                             "," + dtPoint.UnicomRsrp + "," + dtPoint.MobileRsrp + "," +
                                             dtPoint.TelecomRsrp);
                        }
                    }
                }

            }
        }
    }
}
