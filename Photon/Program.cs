using Photon;

ApplicationBuilder builder = new();
builder.UsePlatform<WindowsPlatform>()
       .UseApplication<GameApplication>();

LoggerBuilder loggerBuilder = new();
loggerBuilder.WriteToConsole()
             .Build();

try
{
    using (Application app = builder.Build())
    {
        app.Run();
    }
}
catch (Exception ex)
{
    Logger.WindowsLogger.LogException(ex);
    throw;
}
Logger.Close();
