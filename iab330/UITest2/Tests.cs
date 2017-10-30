using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest2
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
        public void AddNewRoom()
        {         
            app.Tap("addRoomButton");
            app.Tap("roomName");
            app.EnterText("room test");
            app.Back();
            app.Tap("addRoomButton");
            app.WaitForElement(x => x.Marked("room test"));
        }

        [Test]
        public void AddNewBox()
        {
            app.Tap("manageBoxesButton");
            app.Tap("boxName");
            app.EnterText("box test");
            app.Back();
            app.Tap("roomType");
            app.WaitForElement(x => x.Marked("Kitchen"));
            app.Tap("createBox");           
            app.WaitForElement(x => x.Marked("box test"));
        }

        [Test]
        public void AddNewItem()
        {
            app.Tap("manageBoxesButton");
            app.Tap("boxName");
            app.EnterText("box test");
            app.Back();
            app.Tap("roomType");
            app.Tap(x => x.Marked("Kitchen"));
            app.Tap("createBox");
            app.Tap(x => x.Marked("box test"));
            app.Tap("addItemButton");
            app.Tap("itemName");
            app.EnterText("item test");
            app.Back();
            app.Tap("quantity");
            app.EnterText("1");
            app.Back();
            app.Tap("boxes");
            app.Tap(x => x.Marked("box test"));
            app.Tap("addItemButton");
            app.Back();
            app.WaitForElement(x => x.Marked("item test"));
        }
    }
}

