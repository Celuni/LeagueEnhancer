using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Library.Services
{
    public class NotificationService
    {
        public void SendNotification(string message, Action<ToastNotification, object> callback = null)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText03);

            var textNodes = template.GetElementsByTagName("text");
            textNodes.Item(0).InnerText = LeagueEnhancer.AppTitle;
            textNodes.Item(1).InnerText = message;

            var notifier = ToastNotificationManager.CreateToastNotifier(LeagueEnhancer.AppTitle);
            var notification = new ToastNotification(template);
            notifier.Show(notification);

            if (callback != null)
                notification.Activated += (ToastNotification toast, object args) => callback(toast, args);
        }

        public async Task<string> SendNotificationWithInputFieldAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}
