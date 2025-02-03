#pragma once

namespace Photon
{
    public ref class D3D12Application abstract : public Application
    {
    public:
        D3D12Application(AppPlatform^ platform);

    protected:
        ~D3D12Application()
        {
            if (_disposed)
            {
                return;
            }
            this->!D3D12Application();
            _disposed = true;
        }

        void Update(System::TimeSpan deltaTime) override;

        void Render(System::TimeSpan deltaTime) override;

    private:
        bool _disposed;

        !D3D12Application();
    };
}
