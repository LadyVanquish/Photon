#pragma once

namespace Photon
{
    public ref class D3D12Application abstract : public Application
    {
    public:
        D3D12Application(AppPlatform^ platform);
        ~D3D12Application()
        {
            this->!D3D12Application();
        }
        !D3D12Application();
    protected:
        void Render() override;
        virtual void OnRender() abstract;
    };
}
