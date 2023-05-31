#pragma once
#include <Windows.h>

namespace Photon
{
    ref class WindowsPlatform;

    ref class WindowsWindow : public Window
    {
    public:
        property System::String^ Title
        {
            System::String^ get() override
            {
                return _title;
            }
            void set(System::String^ value) override
            {
                _title = value;
            }
        }
        property Size ClientSize
        {
            Size get() override
            {
                return _clientSize;
            }
        }
        property System::IntPtr Handle
        {
            System::IntPtr get() override
            {
                return System::IntPtr(_hWnd);
            }
        }
        property bool InSizeMove
        {
            bool get()
            {
                return _inSizeMove;
            }
        }

        WindowsWindow(WindowsPlatform^ platform, System::String^ title, Size size, HINSTANCE hInstance);

        void Show();

        ~WindowsWindow()
        {
            this->!WindowsWindow();
        }

        !WindowsWindow();

    internal:
        void EnterSizeMove();
        void ExitSizeMove();

    private:
        WindowsPlatform^ _platform;
        HWND _hWnd;
        System::String^ _title;
        Size _clientSize;
        bool _inSizeMove;
    };
}
