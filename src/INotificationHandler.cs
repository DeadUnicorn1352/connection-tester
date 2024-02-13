namespace ServiceTester;

interface INotificationHandler
{
    Task ShowErrorNotification(ErrorNotificationData notificationData);
}