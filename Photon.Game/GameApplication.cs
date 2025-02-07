using Photon.Window;

namespace Photon;

public sealed class GameApplication(string title, Rectangle size) : D3D12Application(title, ref size)
{
    protected override void Update(ref GameTime gameTime)
    {
    }

    protected override void Draw(ref GameTime gameTime)
    {
    }
}
