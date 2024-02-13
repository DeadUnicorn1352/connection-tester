using Microsoft.Toolkit.Uwp.Notifications;
using Serilog;

namespace ServiceTester;

class NotificationHandler : INotificationHandler
{
    public Task ShowErrorNotification(ErrorNotificationData notificationData){
        Log.Logger.Error("Could not connect to the server url: {url}", notificationData.Url);
        new ToastContentBuilder()
                .AddText("Could not connect to the server")
                .AddText($"Check your server info in logs")
                .Show();
        return Task.CompletedTask;
    }
}