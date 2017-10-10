using iab330.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Tests1
{
    [TestFixture]
    public class TestClass
    {
        RoomsViewModel tRooms;
        [SetUp]
        public void Setup()
        {
            tRooms = new RoomsViewModel();
        }
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here


            //Assert.Pass("Your first passing test");
        }
    }
}
