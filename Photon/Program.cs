using Photon;

ApplicationBuilder builder = new();
builder.UsePlatform<WindowsPlatform>()
       .UseApplication<PhotonApplication>();

using (Application app = builder.Build())
{
    app.Run();
}
