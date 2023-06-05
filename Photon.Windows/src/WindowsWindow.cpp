#include "phpch.h"

#include "WindowsWindow.h"
#include "WindowsPlatform.h"

namespace Photon
{
    WindowsWindow::WindowsWindow(WindowsPlatform^ platform, System::String^ title, Window::Rectangle positionAndSize, HINSTANCE hInstance)
    {
        _platform = platform;
        _title = title;

        RECT rect {};
        rect.left = positionAndSize.X == -1 ? (GetSystemMetrics(SM_CXSCREEN) - positionAndSize.Width) / 2 : positionAndSize.X;
        rect.top = positionAndSize.Y == -1 ? (GetSystemMetrics(SM_CYSCREEN) - positionAndSize.Height) / 2 : positionAndSize.Y;
        rect.right = rect.left + positionAndSize.Width;
        rect.bottom = rect.top + positionAndSize.Height;

        PH_INFO("Creating window '{0}' at [{1}, {2}] with size: [{3}, {4}].", title, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

        AdjustWindowRectEx(&rect, WS_OVERLAPPEDWINDOW, false, WS_EX_APPWINDOW);
        _hWnd = CreateWindowEx(WS_EX_APPWINDOW,
                               L"PhotonWindow",
                               CString(title),
                               WS_OVERLAPPEDWINDOW,
                               rect.left,
                               rect.top,
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
        ClientArea = Window::Size(rect.right - rect.left, rect.bottom - rect.top);

        PH_INFO("Created window '{0}' with surface area: [{1}, {2}].", title, ClientArea.Width, ClientArea.Height);
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
        ClientArea = Window::Size(rect.right - rect.left, rect.bottom - rect.top);
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
