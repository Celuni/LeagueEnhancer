using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Library.Modules
{
    public class NotificationManager : IBaseModule
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            ShowMessage("YAY");
            ShowMessageTest();
        }

        private void ShowMessageTest()
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            var textNodes = template.GetElementsByTagName("text");
            textNodes.Item(0).InnerText = "I'm a toast notification!";

            var notifier = ToastNotificationManager.CreateToastNotifier("My .NET Core App");
            var notification = new ToastNotification(template);
            notifier.Show(notification);
        }

        public static void ShowMessage(string message)
        {
            ToastContent toastContent = new ToastContent
            {
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Test"
                            }
                        },
                        //AppLogoOverride = new ToastGenericAppLogo
                        //{
                        //      Source = Properties.Resources.Icon
                        //}
                        Attribution = new ToastGenericAttributionText
                        {
                            Text = "Via League Enhancer"
                        }
                    }
                },
                Launch = "focus",
                ActivationType = ToastActivationType.Foreground
            };

            var toast = SendNotification(toastContent);
            Console.WriteLine("tasd");
        }

        private static ToastNotifier NOTIFIER = ToastNotificationManager.CreateToastNotifier("App.AppId");
        private static int COUNTER;

        private static object SendNotification(ToastContent content)
        {
            var xml = new XmlDocument();
            xml.LoadXml(content.GetContent());

            var toast = new ToastNotification(xml)
            {
                Tag = "" + COUNTER++
            };

            NOTIFIER.Show(toast);
            return toast;
        }
    }
}
