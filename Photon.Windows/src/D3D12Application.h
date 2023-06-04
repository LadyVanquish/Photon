#pragma once

namespace Photon
{
    public ref class D3D12Application abstract : public Application
    {
    public:
        D3D12Application(AppPlatform^ platform);
        ~D3D12Application() override
        {
            this->!D3D12Application();
        }
    protected:
        !D3D12Application();
        void Render() override;
        virtual void OnRender() abstract;
    };
}
