#include "WindowsWindow.h"
#include "WindowsPlatform.h"
#include <system_error>

namespace Photon
{
    WindowsWindow::WindowsWindow(WindowsPlatform^ platform, System::String^ title, Size size, HINSTANCE hInstance)
    {
        _platform = platform;
        _title = title;
        RECT rect {};
        rect.right = size.Width;
        rect.bottom = size.Height;

        AdjustWindowRectEx(&rect, WS_OVERLAPPEDWINDOW, false, WS_EX_APPWINDOW);
        _hWnd = CreateWindowEx(WS_EX_APPWINDOW,
                               L"PhotonWindow",
                               CString(title),
                               WS_OVERLAPPEDWINDOW,
                               CW_USEDEFAULT,
                               CW_USEDEFAULT,
                               rect.right - rect.left,
                               rect.bottom - rect.top,
                               NULL,
                               NULL,
                               hInstance,
                               NULL);
        if (!_hWnd)
        {
            std::string message = std::string("Failed to create window. Error: ") + std::system_category().message(GetLastError());
            throw gcnew System::InvalidOperationException(gcnew System::String(message.c_str()));
        }

        GetClientRect(_hWnd, &rect);
        _clientSize = Size(rect.right - rect.left, rect.bottom - rect.top);
    }

    void WindowsWindow::EnterSizeMove()
    {
        _inSizeMove = true;
    }

    void WindowsWindow::ExitSizeMove()
    {
        _inSizeMove = false;
        RECT rect {};
        GetClientRect(_hWnd, &rect);
        _clientSize = Size(rect.right - rect.left, rect.bottom - rect.top);
    }

    void WindowsWindow::Show()
    {
        ShowWindow(_hWnd, SW_SHOWNORMAL);
    }

    WindowsWindow::!WindowsWindow()
    {
        DestroyWindow(_hWnd);
        _hWnd = NULL;
    }
}
