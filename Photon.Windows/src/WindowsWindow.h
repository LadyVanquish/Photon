#pragma once

namespace Photon
{
    using namespace Events;

    ref class WindowsPlatform;

    ref class WindowsWindow : public PhotonWindow
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
                SetWindowText(_hWnd, CString(_title));
            }
        }
        property bool VSync
        {
            bool get() override
            {
                return _vSync;
            }
            void set(bool value) override
            {
                _vSync = value;
            }
                }
        property float DPI
        {
            float get()
            {
                return _dpi;
            }
        }
        property bool InSizeMove
        {
            bool get()
            {
                return _inSizeMove;
            }
        }

        WindowsWindow(WindowsPlatform^ platform, System::String^ title, Window::Rectangle positionAndSize, HINSTANCE hInstance);

        void OnEvent(PhotonEvent^ args)
        {
            PH_INFO(args->ToString());
            ProcessEvent(args);
        }

        void Show();

    protected:
        ~WindowsWindow()
        {
            if (_disposed)
            {
                return;
            }
            this->!WindowsWindow();
            _disposed = true;
        }

    internal:
        HWND _hWnd;

        property LONG WindowStyle
        {
            LONG get()
            {
                return GetWindowLong(_hWnd, GWL_STYLE);
            }
            void set(LONG value)
            {
                SetWindowLong(_hWnd, GWL_STYLE, value);
            }
        }
        property LONG WindowExStyle
        {
            LONG get()
            {
                return GetWindowLong(_hWnd, GWL_EXSTYLE);
            }
            void set(LONG value)
            {
                SetWindowLong(_hWnd, GWL_EXSTYLE, value);
            }
        }

        void EnterSizeMove();
        
        void ExitSizeMove();

    private:
        WindowsPlatform^ _platform;
        System::String^ _title;
        bool _vSync;
        bool _inSizeMove;
        float _dpi;
        bool _disposed;

        !WindowsWindow();
    };
}
