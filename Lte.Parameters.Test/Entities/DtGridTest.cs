using System;
using System.IO;
using System.Xml;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class DtGridTest
    {
        private string _xmlFileName;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _xmlFileName = Path.Combine(excelFilesDirectory, "FS禅城RSRP渲染情况.xml");
        }

        [Test]
        public void Test_ReadXmlFile()
        {
            var xml = new XmlDocument();
            xml.Load(new StreamReader(_xmlFileName));
            var childs = xml.ChildNodes[1].ChildNodes[0].ChildNodes;
            for (var i = 0; i < childs.Count; i++)
            {
                var node = childs[i];
                if (node.Name != "Folder") continue;
                Console.WriteLine(node.ChildNodes[0].InnerText);
                for (var j = 1; j < node.ChildNodes.Count; j++)
                {
                    var subNode = node.ChildNodes[j];
                    Console.WriteLine(subNode.ChildNodes[0].InnerText);
                    for (var k = 1; k < subNode.ChildNodes.Count; k++)
                    {
                        var placement = subNode.ChildNodes[k];
                        var polygon = placement.ChildNodes[2];
                        var bound = polygon.ChildNodes[2];
                        var coordinates = bound.FirstChild.FirstChild;
                        Console.WriteLine(coordinates.InnerText);
                    }
                }
            }
        }

        [Test]
        public void Test_ReadXml_WithClass()
        {
            var xml = new XmlDocument();
            xml.Load(new StreamReader(_xmlFileName));
            var list = MrGridXml.ReadGridXmls(xml, "禅城");
            foreach (var item in list)
            {
                Console.WriteLine(item.StatDate.ToString("yyyyMMdd") + "," + item.District + "," + item.Description +
                                  "," + item.Frequency + "," + item.Coordinates);
            }
        }
    }
}
