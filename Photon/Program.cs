using Photon;

ApplicationBuilder builder = new();
builder.UseApplication<GameApplication>();

LoggerBuilder loggerBuilder = new();
loggerBuilder.WriteToConsole()
             .Build();

#if !DEBUG
try
{
#endif
using (Application app = builder.Build())
{
    app.Run();
}
#if !DEBUG
}
catch (Exception ex)
{
    Logger.WindowsLogger.LogException(ex);
    throw;
}
#endif
Logger.Close();
