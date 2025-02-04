#pragma once

#include "WindowsWindow.h"

namespace Photon
{
    public ref class WindowsPlatform : public AppPlatform
    {
    public:
        WindowsPlatform(System::String^ title, Window::Rectangle size);

        property bool IsBlockingRun
        {
            bool get() override
            {
                return true;
            }
        }
        property PhotonWindow^ MainWindow
        {
            PhotonWindow^ get() override
            {
                return _mainWindow;
            }
        }

        void Run() override;
        void RequestExit() override;

    protected:
        ~WindowsPlatform() override
        {
            if (_disposed)
            {
                return;
            }
            this->!WindowsPlatform();
            _disposed = true;
        }

    internal:
        static System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>^ Windows = gcnew System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>(5);

    private:
        WindowsWindow^ _mainWindow;
        bool _disposed;

        !WindowsPlatform();
    };
}
