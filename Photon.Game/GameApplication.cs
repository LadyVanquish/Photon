namespace Photon;

public sealed class GameApplication(AppPlatform platform) : D3D12Application(platform)
{
    protected override void OnRender()
    {
        throw new NotImplementedException();
    }
}
