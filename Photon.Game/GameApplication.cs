namespace Photon;

public sealed class GameApplication(AppPlatform platform) : D3D12Application(platform)
{
    protected override void Update(TimeSpan deltaTime)
    {
        throw new NotImplementedException();
    }

    protected override void Render(TimeSpan deltaTime)
    {
        throw new NotImplementedException();
    }
}
