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

    internal:
        static System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>^ Windows = gcnew System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>(5);

    private:

        bool _disposed;
        WindowsWindow^ _mainWindow;
    };
}
