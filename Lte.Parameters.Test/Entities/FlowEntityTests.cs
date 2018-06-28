using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.MySqlFramework.Entities;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class QciEntityTests
    {
        [Test]
        public void QciViewTest()
        {
            var view = new QciView
            {
                Cqi0Times = 10,
                Cqi1Times = 10,
                Cqi2Times = 10,
                Cqi3Times = 10,
                Cqi4Times = 10,
                Cqi5Times = 10,
                Cqi6Times = 10,
                Cqi7Times = 10,
                Cqi8Times = 10,
                Cqi9Times = 10,
                Cqi10Times = 10,
                Cqi11Times = 10,
                Cqi12Times = 10,
                Cqi13Times = 10,
                Cqi14Times = 10,
                Cqi15Times = 0
            };
            view.CqiCounts.Item1.ShouldBe(70);
            view.CqiCounts.Item2.ShouldBe(80);
        }

        [Test]
        public void TownQciViewTest()
        {
            var view=new TownQciView
            {
                Cqi0Times = 100,
                Cqi1Times = 1000,
                Cqi2Times = 10,
                Cqi3Times = 10,
                Cqi4Times = 10,
                Cqi5Times = 10,
                Cqi6Times = 10,
                Cqi7Times = 10,
                Cqi8Times = 10,
                Cqi9Times = 10,
                Cqi10Times = 10,
                Cqi11Times = 10,
                Cqi12Times = 10,
                Cqi13Times = 10,
                Cqi14Times = 10,
                Cqi15Times = 0
            };
            view.CqiCounts.Item1.ShouldBe(1150);
            view.CqiCounts.Item2.ShouldBe(80);
        }

        [Test]
        public void DistrictQciViewTest()
        {
            var view = new DistrictQciView
            {
                Cqi0Times = 100,
                Cqi1Times = 1000,
                Cqi2Times = 10,
                Cqi3Times = 10,
                Cqi4Times = 10,
                Cqi5Times = 10,
                Cqi6Times = 10,
                Cqi7Times = 10,
                Cqi8Times = 10,
                Cqi9Times = 10,
                Cqi10Times = 10,
                Cqi11Times = 10,
                Cqi12Times = 10,
                Cqi13Times = 10,
                Cqi14Times = 10,
                Cqi15Times = 0
            };
            view.CqiCounts.Item1.ShouldBe(1150);
            view.CqiCounts.Item2.ShouldBe(80);
        }
    }
}
