using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest2
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../iab330/iab330.Android/bin/Debug/com.companyname.iab330.apk")
                    .StartApp();
            }

            return ConfigureApp
               .iOS
               .StartApp();
        }
    }
}

