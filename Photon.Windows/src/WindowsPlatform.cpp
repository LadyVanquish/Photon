#include "phpch.h"

#include "WindowsPlatform.h"

namespace Photon
{
    static int GetVirtualKey(WPARAM wParam, LPARAM lParam)
    {
        PH_INFO("ScanCode pressed: {0:x8}", lParam);
        int virtualKey = (int)wParam;
        int scanCode = 0;
        int keyData = (int)lParam;

        if (virtualKey == VK_SHIFT)
        {
            scanCode = (keyData & 0xFF0000) >> 16;
            virtualKey = MapVirtualKey(scanCode, MAPVK_VSC_TO_VK_EX);
            if (virtualKey == 0)
            {
                virtualKey = VK_LSHIFT;
            }
        }

        if (virtualKey == VK_MENU)
        {
            bool right = (keyData & 0x01000000) >> 24;
            if (right)
            {
                virtualKey = VK_RMENU;
            }
            else
            {
                virtualKey = VK_LMENU;
            }
        }

        if (virtualKey == VK_CONTROL)
        {
            bool right = (keyData & 0x01000000) >> 24;
            if (right)
            {
                virtualKey = VK_RCONTROL;
            }
            else
            {
                virtualKey = VK_LCONTROL;
            }
        }

        return virtualKey;
    }

    static int GetScanCode(WPARAM wParam, LPARAM lParam)
    {
        int keyData = (int)lParam;
        int scanCode = (keyData & 0xFF0000) >> 16;
        if (scanCode == 0)
        {
            int virtualKey = GetVirtualKey(wParam, lParam);
            scanCode = MapVirtualKey(virtualKey, 0);
        }
        return scanCode;
    }

    static bool IsExtendedKey(LPARAM lParam)
    {
        return (int)lParam & 0x01000000;
    }

    static KeyboardKey KeyFromVirtualKey(int virtualKey)
    {
        switch (virtualKey)
        {
            case VK_CANCEL:
                return KeyboardKey::Cancel;
            case VK_BACK:
                return KeyboardKey::Backspace;
            case VK_TAB:
                return KeyboardKey::Tab;
            case VK_CLEAR:
                return KeyboardKey::Clear;
            case VK_RETURN:
                return KeyboardKey::Enter;
            case VK_PAUSE:
                return KeyboardKey::Pause;
            case VK_CAPITAL:
                return KeyboardKey::CapsLock;
            case VK_KANA:
                return KeyboardKey::KanaMode;
            case VK_JUNJA:
                return KeyboardKey::JunjaMode;
            case VK_FINAL:
                return KeyboardKey::FinalMode;
            case VK_KANJI:
                return KeyboardKey::KanjiMode;
            case VK_ESCAPE:
                return KeyboardKey::Escape;
            case VK_CONVERT:
                PH_WARNING("Key VK_CONVERT has no mapping.");
                return KeyboardKey::None; // TODO:
            case VK_NONCONVERT:
                PH_WARNING("Key VK_NONCONVERT has no mapping.");
                return KeyboardKey::None; // TODO:
            case VK_ACCEPT:
                PH_WARNING("Key VK_ACCEPT has no mapping.");
                return KeyboardKey::None; // TODO:
            case VK_MODECHANGE:
                return KeyboardKey::ModeChange;
            case VK_SPACE:
                return KeyboardKey::Space;
            case VK_PRIOR:
                return KeyboardKey::PageUp;
            case VK_NEXT:
                return KeyboardKey::PageDown;
            case VK_END:
                return KeyboardKey::End;
            case VK_HOME:
                return KeyboardKey::Home;
            case VK_LEFT:
                return KeyboardKey::Left;
            case VK_UP:
                return KeyboardKey::Up;
            case VK_RIGHT:
                return KeyboardKey::Right;
            case VK_DOWN:
                return KeyboardKey::Down;
            case VK_SELECT:
                return KeyboardKey::Select;
            case VK_PRINT:
                return KeyboardKey::Print;
            case VK_EXECUTE:
                return KeyboardKey::Execute;
            case VK_SNAPSHOT:
                return KeyboardKey::PrintScreen;
            case VK_INSERT:
                return KeyboardKey::Insert;
            case VK_DELETE:
                return KeyboardKey::Delete;
            case VK_HELP:
                return KeyboardKey::Help;
            case '0':
                return KeyboardKey::D0;
            case '1':
                return KeyboardKey::D1;
            case '2':
                return KeyboardKey::D2;
            case '3':
                return KeyboardKey::D3;
            case '4':
                return KeyboardKey::D4;
            case '5':
                return KeyboardKey::D5;
            case '6':
                return KeyboardKey::D6;
            case '7':
                return KeyboardKey::D7;
            case '8':
                return KeyboardKey::D8;
            case '9':
                return KeyboardKey::D9;
            case 'A':
                return KeyboardKey::A;
            case 'B':
                return KeyboardKey::B;
            case 'C':
                return KeyboardKey::C;
            case 'D':
                return KeyboardKey::D;
            case 'E':
                return KeyboardKey::E;
            case 'F':
                return KeyboardKey::F;
            case 'G':
                return KeyboardKey::G;
            case 'H':
                return KeyboardKey::H;
            case 'I':
                return KeyboardKey::I;
            case 'J':
                return KeyboardKey::J;
            case 'K':
                return KeyboardKey::K;
            case 'L':
                return KeyboardKey::L;
            case 'M':
                return KeyboardKey::M;
            case 'N':
                return KeyboardKey::N;
            case 'O':
                return KeyboardKey::O;
            case 'P':
                return KeyboardKey::P;
            case 'Q':
                return KeyboardKey::Q;
            case 'R':
                return KeyboardKey::R;
            case 'S':
                return KeyboardKey::S;
            case 'T':
                return KeyboardKey::T;
            case 'U':
                return KeyboardKey::U;
            case 'V':
                return KeyboardKey::V;
            case 'W':
                return KeyboardKey::W;
            case 'X':
                return KeyboardKey::X;
            case 'Y':
                return KeyboardKey::Y;
            case 'Z':
                return KeyboardKey::Z;
            case VK_LWIN:
                return KeyboardKey::LWin;
            case VK_RWIN:
                return KeyboardKey::RWin;
            case VK_APPS:
                return KeyboardKey::Apps;
            case VK_SLEEP:
                return KeyboardKey::Sleep;
            case VK_NUMPAD0:
                return KeyboardKey::NumPad0;
            case VK_NUMPAD1:
                return KeyboardKey::NumPad1;
            case VK_NUMPAD2:
                return KeyboardKey::NumPad2;
            case VK_NUMPAD3:
                return KeyboardKey::NumPad3;
            case VK_NUMPAD4:
                return KeyboardKey::NumPad4;
            case VK_NUMPAD5:
                return KeyboardKey::NumPad5;
            case VK_NUMPAD6:
                return KeyboardKey::NumPad6;
            case VK_NUMPAD7:
                return KeyboardKey::NumPad7;
            case VK_NUMPAD8:
                return KeyboardKey::NumPad8;
            case VK_NUMPAD9:
                return KeyboardKey::NumPad9;
            case VK_MULTIPLY:
                return KeyboardKey::NumPadMultiply;
            case VK_ADD:
                return KeyboardKey::NumPadAdd;
            case VK_SEPARATOR:
                return KeyboardKey::NumPadSeparator;
            case VK_SUBTRACT:
                return KeyboardKey::NumPadSubtract;
            case VK_DECIMAL:
                return KeyboardKey::NumPadDecimal;
            case VK_DIVIDE:
                return KeyboardKey::NumPadDivide;
            case VK_F1:
                return KeyboardKey::F1;
            case VK_F2:
                return KeyboardKey::F2;
            case VK_F3:
                return KeyboardKey::F3;
            case VK_F4:
                return KeyboardKey::F4;
            case VK_F5:
                return KeyboardKey::F5;
            case VK_F6:
                return KeyboardKey::F6;
            case VK_F7:
                return KeyboardKey::F7;
            case VK_F8:
                return KeyboardKey::F8;
            case VK_F9:
                return KeyboardKey::F9;
            case VK_F10:
                return KeyboardKey::F10;
            case VK_F11:
                return KeyboardKey::F11;
            case VK_F12:
                return KeyboardKey::F12;
            case VK_F13:
                return KeyboardKey::F13;
            case VK_F14:
                return KeyboardKey::F14;
            case VK_F15:
                return KeyboardKey::F15;
            case VK_F16:
                return KeyboardKey::F16;
            case VK_F17:
                return KeyboardKey::F17;
            case VK_F18:
                return KeyboardKey::F18;
            case VK_F19:
                return KeyboardKey::F19;
            case VK_F20:
                return KeyboardKey::F20;
            case VK_F21:
                return KeyboardKey::F21;
            case VK_F22:
                return KeyboardKey::F22;
            case VK_F23:
                return KeyboardKey::F23;
            case VK_F24:
                return KeyboardKey::F24;
            case VK_NUMLOCK:
                return KeyboardKey::NumLock;
            case VK_SCROLL:
                return KeyboardKey::ScrollLock;
            case VK_SHIFT:
            case VK_LSHIFT:
                return KeyboardKey::LShift;
            case VK_RSHIFT:
                return KeyboardKey::RShift;
            case VK_CONTROL:
            case VK_LCONTROL:
                return KeyboardKey::LControl;
            case VK_RCONTROL:
                return KeyboardKey::RControl;
            case VK_MENU:
            case VK_LMENU:
                return KeyboardKey::LMenu;
            case VK_RMENU:
                return KeyboardKey::RMenu;
            case VK_BROWSER_BACK:
                return KeyboardKey::BrowserBack;
            case VK_BROWSER_FORWARD:
                return KeyboardKey::BrowserForward;
            case VK_BROWSER_REFRESH:
                return KeyboardKey::BrowserRefresh;
            case VK_BROWSER_STOP:
                return KeyboardKey::BrowserStop;
            case VK_BROWSER_SEARCH:
                return KeyboardKey::BrowserSearch;
            case VK_BROWSER_FAVORITES:
                return KeyboardKey::BrowserFavorites;
            case VK_BROWSER_HOME:
                return KeyboardKey::BrowserHome;
            case VK_VOLUME_MUTE:
                return KeyboardKey::VolumeMute;
            case VK_VOLUME_DOWN:
                return KeyboardKey::VolumeDown;
            case VK_VOLUME_UP:
                return KeyboardKey::VolumeUp;
            case VK_MEDIA_NEXT_TRACK:
                return KeyboardKey::MediaNextTrack;
            case VK_MEDIA_PREV_TRACK:
                return KeyboardKey::MediaPreviousTrack;
            case VK_MEDIA_STOP:
                return KeyboardKey::MediaStop;
            case VK_MEDIA_PLAY_PAUSE:
                return KeyboardKey::MediaPlayPause;
            case VK_LAUNCH_MAIL:
                return KeyboardKey::LaunchMail;
            case VK_LAUNCH_MEDIA_SELECT:
                return KeyboardKey::SelectMedia;
            case VK_LAUNCH_APP1:
                return KeyboardKey::LaunchApplication1;
            case VK_LAUNCH_APP2:
                return KeyboardKey::LaunchApplication2;
            case VK_OEM_1:
                return KeyboardKey::Semicolon;
            case VK_OEM_PLUS:
                return KeyboardKey::Add;
            case VK_OEM_COMMA:
                return KeyboardKey::Comma;
            case VK_OEM_MINUS:
                return KeyboardKey::Subtract;
            case VK_OEM_PERIOD:
                return KeyboardKey::Period;
            case VK_OEM_2:
                return KeyboardKey::Slash;
            case VK_OEM_3:
                return KeyboardKey::Grave;
            case VK_OEM_4:
                return KeyboardKey::LBracket;
            case VK_OEM_5:
                return KeyboardKey::Backslash;
            case VK_OEM_6:
                return KeyboardKey::RBracket;
            case VK_OEM_7:
                return KeyboardKey::Quote;
            case VK_OEM_AX:
                return KeyboardKey::OemAx;
            case VK_OEM_102:
                return KeyboardKey::IntlBackslash;
            case VK_PROCESSKEY:
                return KeyboardKey::ProcessKey;
            case VK_OEM_ATTN:
                return KeyboardKey::Attn;
            case VK_OEM_FINISH:
                return KeyboardKey::Katakana;
            case VK_OEM_COPY:
                return KeyboardKey::Hiragana;
                // VK_OEM_ENLW
                // VK_OEM_BACKTAB
            case VK_ATTN:
                return KeyboardKey::Attn;
            case VK_CRSEL:
                return KeyboardKey::Crsel;
            case VK_EXSEL:
                return KeyboardKey::Exsel;
            case VK_EREOF:
                return KeyboardKey::EraseEof;
            case VK_PLAY:
                return KeyboardKey::Play;
            case VK_ZOOM:
                return KeyboardKey::Zoom;
                // VK_NONAME
            case VK_PA1:
                return KeyboardKey::Pa1;
            case VK_OEM_CLEAR:
                return KeyboardKey::OemClear;
            default:
                PH_WARNING("TODO: Key '{0}' has no mapping.", virtualKey);
        }
        return KeyboardKey::None;
    }

    static LRESULT ProcessWindowMessage(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
    {
        WindowsWindow^ window;
        if (!WindowsPlatform::Windows->TryGetValue(System::IntPtr(hWnd), window))
        {
            return DefWindowProc(hWnd, message, wParam, lParam);
        }
        switch (message)
        {
            case WM_MOUSEMOVE:
                window->OnEvent(gcnew Events::MouseMovedEvent((float)GET_X_LPARAM(lParam), (float)GET_Y_LPARAM(lParam)));
                return 0;
            case WM_LBUTTONDOWN:
                window->OnEvent(gcnew Events::MouseButtonPressedEvent(MouseButton::Left));
                return 0;
            case WM_LBUTTONUP:
                window->OnEvent(gcnew Events::MouseButtonReleasedEvent(MouseButton::Left));
                return 0;
            case WM_RBUTTONDOWN:
                window->OnEvent(gcnew Events::MouseButtonPressedEvent(MouseButton::Right));
                return 0;
            case WM_RBUTTONUP:
                window->OnEvent(gcnew Events::MouseButtonReleasedEvent(MouseButton::Right));
                return 0;
            case WM_MBUTTONDOWN:
                window->OnEvent(gcnew Events::MouseButtonPressedEvent(MouseButton::Middle));
                return 0;
            case WM_MBUTTONUP:
                window->OnEvent(gcnew Events::MouseButtonReleasedEvent(MouseButton::Middle));
                return 0;
            case WM_XBUTTONDOWN:
                window->OnEvent(gcnew Events::MouseButtonPressedEvent((MouseButton)((int)MouseButton::X1 - 1 + GET_XBUTTON_WPARAM(wParam))));
                return 0;
            case WM_XBUTTONUP:
                window->OnEvent(gcnew Events::MouseButtonReleasedEvent((MouseButton)((int)MouseButton::X1 - 1 + GET_XBUTTON_WPARAM(wParam))));
                return 0;
            case WM_MOUSEWHEEL:
                window->OnEvent(gcnew Events::MouseScrolledEvent((float)GET_WHEEL_DELTA_WPARAM(wParam) / WHEEL_DELTA, 0.0f));
                return 0;
            case WM_MOUSEHWHEEL:
                window->OnEvent(gcnew Events::MouseScrolledEvent(0.0f, (float)GET_WHEEL_DELTA_WPARAM(wParam) / WHEEL_DELTA));
                return 0;
            case WM_KEYDOWN:
            case WM_SYSKEYDOWN:
                window->OnEvent(gcnew Events::KeyPressedEvent((int)lParam & 0xFF, ((int)lParam & 0x00FF0000) >> 16, KeyFromVirtualKey(GetVirtualKey(wParam, lParam))));
                return 0;
            case WM_KEYUP:
            case WM_SYSKEYUP:
                window->OnEvent(gcnew Events::KeyReleasedEvent(((int)lParam & 0x00FF0000) >> 16, KeyFromVirtualKey(GetVirtualKey(wParam, lParam))));
                return 0;
            case WM_ACTIVATE:
                switch (LOWORD(wParam))
                {
                    case WA_ACTIVE:
                    case WA_CLICKACTIVE:
                        window->OnEvent(gcnew Events::WindowActivateEvent(window->Title));
                        break;
                    case WA_INACTIVE:
                        window->OnEvent(gcnew Events::WindowDeactivateEvent(window->Title));
                        break;
                }
                return 0;
            case WM_ENTERSIZEMOVE:
                window->EnterSizeMove();
                return 0;
            case WM_EXITSIZEMOVE:
                window->ExitSizeMove();
                return 0;
            case WM_CLOSE:
                window->OnEvent(gcnew Events::WindowCloseEvent());
                DestroyWindow(window->_hWnd);
                return 0;
            case WM_NCDESTROY:
                WindowsPlatform::Windows->Remove(System::IntPtr(hWnd));
                if (WindowsPlatform::Windows->Count == 0)
                {
                    PostQuitMessage(0);
                }
                return 0;
        }
        return DefWindowProc(hWnd, message, wParam, lParam);
    }

    WindowsPlatform::WindowsPlatform(System::String^ title, Window::Rectangle size)
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
        OnExit((int)msg.wParam);
    }

    void WindowsPlatform::RequestExit()
    {
        PostQuitMessage(0);
    }

    WindowsPlatform::!WindowsPlatform()
    {

    }
}
