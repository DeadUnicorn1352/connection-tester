using Serilog;
using Serilog.Formatting.Json;
using ServiceTester;
using System.Net.Http;
using Serilog.Formatting.Display;

AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
    Log.Logger.Error(eventArgs.ExceptionObject as Exception, "Unhalted exception occured");
};

var logTemplate = "[{Timestamp:HH:mm:ss} {Level}] {Message} {Exception}{NewLine}";
Log.Logger = new LoggerConfiguration()
        .WriteTo.File(new MessageTemplateTextFormatter(logTemplate), "logs", rollingInterval: RollingInterval.Day)
        .WriteTo.Console()
        .CreateLogger();
var logger = Log.Logger;

HttpClient client = new HttpClient();
    
string url = args[0];
logger.Information("Running for url: {url}", url);
var isSuccess = await TryConnect();
if (isSuccess){
    Success();
    return;
}
const int maxRetries = 3;
const int retryTimeout = 1000;
for (int retries = 0; retries < maxRetries; retries++){
    logger.Warning("Could not connect, retrying. Url: {url}", url);
    await Task.Delay(retryTimeout * (retries + 1));
    isSuccess = await TryConnect();
    if (isSuccess){
        Success();
        return;
    }
}
var errorData = new ErrorNotificationData(url);
INotificationHandler notificationHandler = new NotificationHandler();
await notificationHandler.ShowErrorNotification(errorData);
Environment.Exit(10);
return;

async Task<bool> TryConnect() {
    try{
        var response = await client.GetAsync(url);
        return response.IsSuccessStatusCode;
    }catch(Exception ex){
        logger.Error("Exception while trying to connect to url: {url}, message: {msg}", url, ex.Message);
        return false;
    }
}

void Success() {
    logger.Information("Succeeded for url: {url}", url);
}