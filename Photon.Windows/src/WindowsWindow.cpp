#include "phpch.h"

#include "WindowsWindow.h"
#include "WindowsPlatform.h"

namespace Photon
{
    WindowsWindow::WindowsWindow(WindowsPlatform^ platform, System::String^ title, Window::Rectangle positionAndSize, HINSTANCE hInstance)
    {
        _platform = platform;
        _title = title;
        if (positionAndSize.X == -1)
        {
            positionAndSize.X = (GetSystemMetrics(SM_CXSCREEN) - positionAndSize.Width) / 2;
        }
        if (positionAndSize.Y == -1)
        {
            positionAndSize.Y = (GetSystemMetrics(SM_CYSCREEN) - positionAndSize.Height) / 2;
        }

        PH_INFO("Creating window '{0}' (Size: [{1}, {2}])", title, positionAndSize.Width, positionAndSize.Height);

        AdjustWindowRectEx(&rect, WS_OVERLAPPEDWINDOW, false, WS_EX_APPWINDOW);
        _hWnd = CreateWindowEx(WS_EX_APPWINDOW,
                               L"PhotonWindow",
                               CString(title),
                               WS_OVERLAPPEDWINDOW,
                               positionAndSize.X,
                               positionAndSize.Y,
                               positionAndSize.Width,
                               positionAndSize.Height,
                               NULL,
                               NULL,
                               hInstance,
                               NULL);
        if (!_hWnd)
        {
            std::string message = std::string("Failed to create window. Error: ") + std::system_category().message(GetLastError());
            throw gcnew System::InvalidOperationException(gcnew System::String(message.c_str()));
        }

        RECT rect {};
        GetClientRect(_hWnd, &rect);
        //_clientSize = Size(rect.right - rect.left, rect.bottom - rect.top);

        PH_INFO("Created window '{0}' (Size: [{1}, {2}])", title, rect.right, rect.bottom);
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
        //_clientSize = Size(rect.right - rect.left, rect.bottom - rect.top);
    }

    void WindowsWindow::Show()
    {
        ShowWindow(_hWnd, SW_SHOWNORMAL);
        _dpi = (float)GetDpiForWindow(_hWnd);
    }

    WindowsWindow::!WindowsWindow()
    {
        if (_hWnd)
        {
            DestroyWindow(_hWnd);
            _hWnd = NULL;
        }
    }
}
