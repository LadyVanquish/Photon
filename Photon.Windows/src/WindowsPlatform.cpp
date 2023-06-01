#include "phpch.h"

#include "WindowsPlatform.h"

namespace Photon
{
    LRESULT ProcessWindowMessage(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
    {
        WindowsWindow^ window;
        if (!WindowsPlatform::Windows->TryGetValue(System::IntPtr(hWnd), window))
        {
            return DefWindowProc(hWnd, message, wParam, lParam);
        }
        switch (message)
        {
            case WM_KEYDOWN:
                // TODO: window->OnEvent(gcnew Events::KeyPressedEvent(LOWORD(lParam), (KeyboardKey)MapVirtualKey((UINT)wParam, MAPVK_VK_TO_VSC)));
                return 0;
            case WM_ENTERSIZEMOVE:
                window->EnterSizeMove();
                return 0;
            case WM_EXITSIZEMOVE:
                window->ExitSizeMove();
                return 0;
            case WM_SIZE:
                window->ClientSize = Size(LOWORD(lParam), HIWORD(lParam));
                window->OnEvent(gcnew Events::WindowResizeEvent(LOWORD(lParam), HIWORD(lParam)));
                return 0;
            case WM_CLOSE:
                window->OnEvent(gcnew Events::WindowCloseEvent());
                DestroyWindow(window->_hWnd);
                return 0;
            case WM_DESTROY:
                PostQuitMessage(0);
                return 0;
        }
        return DefWindowProc(hWnd, message, wParam, lParam);
    }

    WindowsPlatform::WindowsPlatform(System::String^ title, Size size)
    {
        HINSTANCE hInstance = GetModuleHandle(NULL);
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
        Windows->Add(System::IntPtr(_mainWindow->_hWnd), _mainWindow);
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
    }

    void WindowsPlatform::RequestExit()
    {
        PostQuitMessage(0);
    }
}
