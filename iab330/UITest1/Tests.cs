using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest1
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void roomPage()
        {
            app.Tap("addRoomButton");
            app.Tap("roomName");
            app.EnterText("room3");
            app.Back();
            app.Tap("addRoomButton");
            app.WaitForElement(x => x.Marked("NameRow").Text("room3"));
            //app.Screenshot("First screen.");


        }
        [Test]
        public void boxPage()
        {
            app.Tap("manageBoxesButton");

        }
    }
}

