#pragma once

namespace Photon
{
    using namespace Events;

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
                SetWindowText(_hWnd, CString(_title));
            }
        }
        property Size ClientSize
        {
            Size get() override
            {
                return _clientSize;
            }
            void set(Size value) override
            {
                _clientSize = value;
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
        property bool InSizeMove
        {
            bool get()
            {
                return _inSizeMove;
            }
        }

        WindowsWindow(WindowsPlatform^ platform, System::String^ title, Size size, HINSTANCE hInstance);

        void SetEventCallback(System::Action<PhotonEvent^>^ callback) override
        {
            _eventCallback = callback;
        }

        void OnEvent(PhotonEvent^ args)
        {
            PH_INFO(args->ToString());
            if (_eventCallback == nullptr)
            {
                return;
            }
            _eventCallback(args);
        }

        void Show();

        ~WindowsWindow()
        {
            this->!WindowsWindow();
        }

        !WindowsWindow();

    internal:
        HWND _hWnd;

        void EnterSizeMove();
        
        void ExitSizeMove();

    private:
        WindowsPlatform^ _platform;
        System::String^ _title;
        Size _clientSize;
        bool _vSync;
        bool _inSizeMove;
        System::Action<PhotonEvent^>^ _eventCallback;
    };
}
