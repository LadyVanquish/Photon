#pragma once

#include "WindowsWindow.h"

namespace Photon
{
    public ref class WindowsPlatform : public AppPlatform
    {
    public:
        WindowsPlatform(System::String^ title, Size size);
        ~WindowsPlatform();

        property bool IsBlockingRun
        {
            bool get() override
            {
                return true;
            }
        }
        property Window^ MainWindow
        {
            Window^ get() override
            {
                return _mainWindow;
            }
        }

        void Run() override;
        void RequestExit() override;

    private:
        bool _disposed;
        WindowsWindow^ _mainWindow;
        System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>^ _windows;
    };
}