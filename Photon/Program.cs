using Photon;

ApplicationBuilder builder = new();
builder.UsePlatform<WindowsPlatform>()
       .UseApplication<PhotonApplication>();

LoggerBuilder loggerBuilder = new();
loggerBuilder.WriteToConsole()
             .Build();

using (Application app = builder.Build())
{
    app.Run();
}
