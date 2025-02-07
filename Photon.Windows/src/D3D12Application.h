#pragma once

namespace Photon
{
    public ref class D3D12Application abstract : public Application
    {
    public:
        property PhotonWindow^ MainWindow
        {
            PhotonWindow^ get() override
            {
                return _mainWindow;
            }
        }

        D3D12Application(System::String^ title, Window::Rectangle% size);

        void Run() override;

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

        void Update(GameTime% gameTime) override;

        void Draw(GameTime% gameTime) override;

    internal:
        void RequestExit();

    private:
        bool _disposed;
        WindowsWindow^ _mainWindow;
        ATOM _wndClass;

        !D3D12Application();
    };
}
