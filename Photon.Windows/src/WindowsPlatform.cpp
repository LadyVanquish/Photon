#include "WindowsPlatform.h"
#include <Windows.h>
#include <system_error>

namespace Photon
{
    LRESULT ProcessWindowMessage(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
    {
        switch (message)
        {
            case WM_DESTROY:
                PostQuitMessage(0);
                return 0;
        }
        return DefWindowProc(hWnd, message, wParam, lParam);
    }

    WindowsPlatform::WindowsPlatform(System::String^ title, Size size)
    {
        HINSTANCE hInstance = GetModuleHandle(NULL);
        _windows = gcnew System::Collections::Generic::Dictionary<System::IntPtr, WindowsWindow^>(5);
        CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);
        WNDCLASSEX wndClassEx {};
        wndClassEx.cbSize = sizeof(WNDCLASSEX);
        wndClassEx.style = CS_HREDRAW | CS_VREDRAW;
        wndClassEx.lpfnWndProc = ProcessWindowMessage;
        wndClassEx.hInstance = hInstance;
        wndClassEx.hCursor = LoadCursor(NULL, IDC_ARROW);
        wndClassEx.lpszClassName = L"PhotonWindow";
        if (!RegisterClassEx(&wndClassEx))
        {
            std::string message = std::string("Failed to create window class. Error: ") + std::system_category().message(GetLastError());
            throw gcnew System::InvalidOperationException(gcnew System::String(message.c_str()));
        }
        _mainWindow = gcnew WindowsWindow(this, title, size, hInstance);
        _windows->Add(_mainWindow->Handle, _mainWindow);
    }

    WindowsPlatform::~WindowsPlatform()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
    }

    void WindowsPlatform::Run()
    {
        OnReady();

        _mainWindow->Show();

        MSG msg {};
        while (msg.message != WM_QUIT)
        {
            if (PeekMessage(&msg, NULL, WM_NULL, WM_NULL, PM_REMOVE))
            {
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
            else
            {
                Application->Tick();
            }
        }

        CoUninitialize();
    }

    void WindowsPlatform::RequestExit()
    {
        PostQuitMessage(0);
    }
}
