#include "phpch.h"

#include "WindowsWindow.h"
#include "WindowsInput.h"
#include "D3D12Application.h"

namespace Photon
{
    D3D12Application::D3D12Application(System::String^ title, Window::Rectangle% size) : Application(title)
    {
        HINSTANCE hInstance = GetModuleHandle(NULL);
        WNDCLASSEX wndClassEx{};
        wndClassEx.cbSize = sizeof(WNDCLASSEX);
        wndClassEx.style = CS_HREDRAW | CS_VREDRAW;
        wndClassEx.lpfnWndProc = ProcessWindowMessage;
        wndClassEx.hInstance = hInstance;
        wndClassEx.hCursor = LoadCursor(NULL, IDC_ARROW);
        wndClassEx.lpszClassName = L"PhotonWindow";
        if (!(_wndClass = RegisterClassEx(&wndClassEx)))
        {
            std::string message = std::string("Failed to create window class. Error: ") + std::system_category().message(GetLastError());
            throw gcnew System::InvalidOperationException(gcnew System::String(message.c_str()));
        }
        _mainWindow = gcnew WindowsWindow(title, size, hInstance);
        Windows->Add(System::IntPtr(_mainWindow->_hWnd), _mainWindow);
    }

    void D3D12Application::Run()
    {
        OnReady();

        _mainWindow->Show();

        MSG msg{};
        while (msg.message != WM_QUIT)
        {
            if (PeekMessage(&msg, NULL, WM_NULL, WM_NULL, PM_REMOVE))
            {
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
            else
            {
                Tick();
            }
        }
        OnExit((int)msg.wParam);
    }

    void D3D12Application::Update(GameTime% gameTime)
    {

    }

    void D3D12Application::Draw(GameTime% gameTime)
    {

    }

    void D3D12Application::RequestExit()
    {
        PostQuitMessage(0);
    }

    D3D12Application::!D3D12Application()
    {

    }
}
