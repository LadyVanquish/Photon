namespace Photon;

public enum KeyboardKey
{
    Invalid = -1,

    /// <summary>
    /// No key.
    /// </summary>
    None,

    /// <summary>
    /// The cancel key.
    /// </summary>
    Cancel,

    /// <summary>
    /// The backspace key.
    /// </summary>
    Backspace,

    /// <summary>
    /// The tab key.
    /// </summary>
    Tab,

    /// <summary>
    /// The clear key.
    /// </summary>
    Clear,

    /// <summary>
    /// The enter key.
    /// </summary>
    Enter,

    /// <summary>
    /// The pause key.
    /// </summary>
    Pause,

    /// <summary>
    /// The caps lock key.
    /// </summary>
    CapsLock,

    /// <summary>
    /// The Kana mode key.
    /// </summary>
    KanaMode,

    /// <summary>
    /// The Hanja mode key.
    /// </summary>
    HanjaMode,

    /// <summary>
    /// The Kanji mode key.
    /// </summary>
    KanjiMode,

    /// <summary>
    /// The Yen key.
    /// </summary>
    Yen,

    /// <summary>
    /// The Ro key.
    /// </summary>
    Ro,

    /// <summary>
    /// The Katakana key.
    /// </summary>
    Katakana,

    /// <summary>
    /// The Hiragana key.
    /// </summary>
    Hiragana,

    /// <summary>
    /// The Katakana/Hiragana key.
    /// </summary>
    KatakanaHiragana,

    /// <summary>
    /// The Henkan key.
    /// </summary>
    Henkan,

    /// <summary>
    /// The Muhenkan key.
    /// </summary>
    Muhenkan,

    /// <summary>
    /// The japanese comma key.
    /// </summary>
    NumpadJpComma,

    /// <summary>
    /// The escape key.
    /// </summary>
    Escape,

    /// <summary>
    /// The space key.
    /// </summary>
    Space,

    /// <summary>
    /// The page up key.
    /// </summary>
    PageUp,

    /// <summary>
    /// The page down key.
    /// </summary>
    PageDown,

    /// <summary>
    /// The end key.
    /// </summary>
    End,

    /// <summary>
    /// The home key.
    /// </summary>
    Home,

    /// <summary>
    /// The left arrow key.
    /// </summary>
    Left,

    /// <summary>
    /// The up arrow key.
    /// </summary>
    Up,

    /// <summary>
    /// The right arrow key.
    /// </summary>
    Right,

    /// <summary>
    /// The down arrow key.
    /// </summary>
    Down,

    /// <summary>
    /// The select key.
    /// </summary>
    Select,

    /// <summary>
    /// The print key.
    /// </summary>
    Print,

    /// <summary>
    /// The execute key.
    /// </summary>
    Execute,

    /// <summary>
    /// The print screen key.
    /// </summary>
    PrintScreen,

    /// <summary>
    /// The insert key.
    /// </summary>
    Insert,

    /// <summary>
    /// The delete key.
    /// </summary>
    Delete,

    /// <summary>
    /// The equal key.
    /// </summary>
    Equal,

    /// <summary>
    /// The help key.
    /// </summary>
    Help,

    /// <summary>
    /// The 0 key.
    /// </summary>
    D0,

    /// <summary>
    /// The 1 key.
    /// </summary>
    D1,

    /// <summary>
    /// The 2 key.
    /// </summary>
    D2,

    /// <summary>
    /// The 3 key.
    /// </summary>
    D3,

    /// <summary>
    /// The 4 key.
    /// </summary>
    D4,

    /// <summary>
    /// The 5 key.
    /// </summary>
    D5,

    /// <summary>
    /// The 6 key.
    /// </summary>
    D6,

    /// <summary>
    /// The 7 key.
    /// </summary>
    D7,

    /// <summary>
    /// The 8 key.
    /// </summary>
    D8,

    /// <summary>
    /// The 9 key.
    /// </summary>
    D9,

    /// <summary>
    /// The A key.
    /// </summary>
    A,

    /// <summary>
    /// The B key.
    /// </summary>
    B,

    /// <summary>
    /// The C key.
    /// </summary>
    C,

    /// <summary>
    /// The D key.
    /// </summary>
    D,

    /// <summary>
    /// The E key.
    /// </summary>
    E,

    /// <summary>
    /// The F key.
    /// </summary>
    F,

    /// <summary>
    /// The G key.
    /// </summary>
    G,

    /// <summary>
    /// The H key.
    /// </summary>
    H,

    /// <summary>
    /// The I key.
    /// </summary>
    I,

    /// <summary>
    /// The J key.
    /// </summary>
    J,

    /// <summary>
    /// The K key.
    /// </summary>
    K,

    /// <summary>
    /// The L key.
    /// </summary>
    L,

    /// <summary>
    /// The M key.
    /// </summary>
    M,

    /// <summary>
    /// The N key.
    /// </summary>
    N,

    /// <summary>
    /// The O key.
    /// </summary>
    O,

    /// <summary>
    /// The P key.
    /// </summary>
    P,

    /// <summary>
    /// The Q key.
    /// </summary>
    Q,

    /// <summary>
    /// The R key.
    /// </summary>
    R,

    /// <summary>
    /// The S key.
    /// </summary>
    S,

    /// <summary>
    /// The T key.
    /// </summary>
    T,

    /// <summary>
    /// The U key.
    /// </summary>
    U,

    /// <summary>
    /// The V key.
    /// </summary>
    V,

    /// <summary>
    /// The W key.
    /// </summary>
    W,

    /// <summary>
    /// The X key.
    /// </summary>
    X,

    /// <summary>
    /// The Y key.
    /// </summary>
    Y,

    /// <summary>
    /// The Z key.
    /// </summary>
    Z,

    /// <summary>
    /// The left Windows logo key (Microsoft Natural Keyboard).
    /// </summary>
    LWin,

    /// <summary>
    /// The right Windows logo key (Microsoft Natural Keyboard).
    /// </summary>
    RWin,

    /// <summary>
    /// The application key (Microsoft Natural Keyboard).
    /// </summary>
    Apps,

    /// <summary>
    /// The computer sleep key.
    /// </summary>
    Sleep,

    /// <summary>
    /// The computer power key.
    /// </summary>
    Power,

    /// <summary>
    /// The 0 key on the numeric keypad.
    /// </summary>
    NumPad0,

    /// <summary>
    /// The 1 key on the numeric keypad.
    /// </summary>
    NumPad1,

    /// <summary>
    /// The 2 key on the numeric keypad.
    /// </summary>
    NumPad2,

    /// <summary>
    /// The 3 key on the numeric keypad.
    /// </summary>
    NumPad3,

    /// <summary>
    /// The 4 key on the numeric keypad.
    /// </summary>
    NumPad4,

    /// <summary>
    /// The 5 key on the numeric keypad.
    /// </summary>
    NumPad5,

    /// <summary>
    /// The 6 key on the numeric keypad.
    /// </summary>
    NumPad6,

    /// <summary>
    /// The 7 key on the numeric keypad.
    /// </summary>
    NumPad7,

    /// <summary>
    /// The 8 key on the numeric keypad.
    /// </summary>
    NumPad8,

    /// <summary>
    /// The 9 key on the numeric keypad.
    /// </summary>
    NumPad9,

    /// <summary>
    /// The multiply key on the numeric keypad.
    /// </summary>
    NumPadMultiply,

    /// <summary>
    /// The add key on the numeric keypad.
    /// </summary>
    NumPadAdd,

    /// <summary>
    /// The separator key on the numeric keypad.
    /// </summary>
    NumPadSeparator,

    /// <summary>
    /// The subtract key on the numeric keypad.
    /// </summary>
    NumPadSubtract,

    /// <summary>
    /// The decimal key on the numeric keypad.
    /// </summary>
    NumPadDecimal,

    /// <summary>
    /// The divide key on the numeric keypad.
    /// </summary>
    NumPadDivide,

    /// <summary>
    /// The enter key on the numeric keypad.
    /// </summary>
    NumPadEnter,

    /// <summary>
    /// The equal key on the numeric keypad.
    /// </summary>
    NumPadEqual,

    /// <summary>
    /// The plus/minus key on the numeric keypad.
    /// </summary>
    NumPadPlusMinus,

    /// <summary>
    /// 
    /// </summary>
    NumPadLeftParen,

    /// <summary>
    /// 
    /// </summary>
    NumPadRightParen,

    /// <summary>
    /// The F1 key.
    /// </summary>
    F1,

    /// <summary>
    /// The F2 key.
    /// </summary>
    F2,

    /// <summary>
    /// The F3 key.
    /// </summary>
    F3,

    /// <summary>
    /// The F4 key.
    /// </summary>
    F4,

    /// <summary>
    /// The F5 key.
    /// </summary>
    F5,

    /// <summary>
    /// The F6 key.
    /// </summary>
    F6,

    /// <summary>
    /// The F7 key.
    /// </summary>
    F7,

    /// <summary>
    /// The F8 key.
    /// </summary>
    F8,

    /// <summary>
    /// The F9 key.
    /// </summary>
    F9,

    /// <summary>
    /// The F10 key.
    /// </summary>
    F10,

    /// <summary>
    /// The F11 key.
    /// </summary>
    F11,

    /// <summary>
    /// The F12 key.
    /// </summary>
    F12,

    /// <summary>
    /// The F13 key.
    /// </summary>
    F13,

    /// <summary>
    /// The F14 key.
    /// </summary>
    F14,

    /// <summary>
    /// The F15 key.
    /// </summary>
    F15,

    /// <summary>
    /// The F16 key.
    /// </summary>
    F16,

    /// <summary>
    /// The F17 key.
    /// </summary>
    F17,

    /// <summary>
    /// The F18 key.
    /// </summary>
    F18,

    /// <summary>
    /// The F19 key.
    /// </summary>
    F19,

    /// <summary>
    /// The F20 key.
    /// </summary>
    F20,

    /// <summary>
    /// The F21 key.
    /// </summary>
    F21,

    /// <summary>
    /// The F22 key.
    /// </summary>
    F22,

    /// <summary>
    /// The F23 key.
    /// </summary>
    F23,

    /// <summary>
    /// The F24 key.
    /// </summary>
    F24,

    /// <summary>
    /// The NUM LOCK key.
    /// </summary>
    NumLock,

    /// <summary>
    /// The SCROLL LOCK key.
    /// </summary>
    ScrollLock,

    /// <summary>
    /// The left SHIFT key.
    /// </summary>
    LShift,

    /// <summary>
    /// The right SHIFT key.
    /// </summary>
    RShift,

    /// <summary>
    /// The left CTRL key.
    /// </summary>
    LControl,

    /// <summary>
    /// The right CTRL key.
    /// </summary>
    RControl,

    /// <summary>
    /// The left ALT key.
    /// </summary>
    LMenu,

    /// <summary>
    /// The right ALT key.
    /// </summary>
    RMenu,

    /// <summary>
    /// The mode change key.
    /// </summary>
    ModeChange,

    /// <summary>
    /// The OEM semicolon key on a US standard keyboard (US ;: ).
    /// </summary>
    Semicolon,

    /// <summary>
    /// The OEM plus key on any country/region keyboard.
    /// </summary>
    Add,

    /// <summary>
    /// The OEM comma key on any country/region keyboard.
    /// </summary>
    Comma,

    /// <summary>
    /// The OEM minus key on any country/region keyboard.
    /// </summary>
    Subtract,

    /// <summary>
    /// The OEM period key on any country/region keyboard.
    /// </summary>
    Period,

    /// <summary>
    /// The OEM question mark key on a US standard keyboard (US /? ).
    /// </summary>
    Slash,

    /// <summary>
    /// The OEM tilde key on a US standard keyboard (US `~ ).
    /// </summary>
    Grave,

    /// <summary>
    /// The OEM open brackets key on a US standard keyboard (US [{ ).
    /// </summary>
    LBracket,

    /// <summary>
    /// The OEM pipe key on a US standard keyboard (US \| ).
    /// </summary>
    Backslash,

    /// <summary>
    /// The OEM close bracket key on a US standard keyboard (US ]} ).
    /// </summary>
    RBracket,

    /// <summary>
    /// The OEM single/double quote key on a US standard keyboard (US '" ).
    /// </summary>
    Quote,

    /// <summary>
    /// The japanese "AX" key.
    /// </summary>
    OemAx,

    /// <summary>
    /// OEM angle bracket or backslash key on the RT 102 key keyboard ( <> or \| ).
    /// </summary>
    IntlBackslash,

    /// <summary>
    /// The browser back key.
    /// </summary>
    BrowserBack,

    /// <summary>
    /// The browser forward key.
    /// </summary>
    BrowserForward,

    /// <summary>
    /// The browser refresh key.
    /// </summary>
    BrowserRefresh,

    /// <summary>
    /// The browser stop key.
    /// </summary>
    BrowserStop,

    /// <summary>
    /// The browser search key.
    /// </summary>
    BrowserSearch,

    /// <summary>
    /// The browser favorites key.
    /// </summary>
    BrowserFavorites,

    /// <summary>
    /// The browser home key.
    /// </summary>
    BrowserHome,

    /// <summary>
    /// The volume mute key.
    /// </summary>
    VolumeMute,

    /// <summary>
    /// The volume down key.
    /// </summary>
    VolumeDown,

    /// <summary>
    /// The volume up key.
    /// </summary>
    VolumeUp,

    /// <summary>
    /// The media next track key.
    /// </summary>
    MediaNextTrack,

    /// <summary>
    /// The media previous track key.
    /// </summary>
    MediaPreviousTrack,

    /// <summary>
    /// The media Stop key.
    /// </summary>
    MediaStop,

    /// <summary>
    /// The media play pause key.
    /// </summary>
    MediaPlayPause,

    /// <summary>
    /// The launch mail key.
    /// </summary>
    LaunchMail,

    /// <summary>
    /// The select media key.
    /// </summary>
    SelectMedia,

    /// <summary>
    /// The start application one key.
    /// </summary>
    LaunchApplication1,

    /// <summary>
    /// The start application two key.
    /// </summary>
    LaunchApplication2,

    /// <summary>
    /// The PROCSS KEY key.
    /// </summary>
    ProcessKey,

    /// <summary>
    /// Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.
    /// </summary>
    Packet,

    /// <summary>
    /// The ATTN key.
    /// </summary>
    Attn,

    /// <summary>
    /// The CRSEL key.
    /// </summary>
    Crsel,

    /// <summary>
    /// The EXSEL key.
    /// </summary>
    Exsel,

    /// <summary>
    /// The ERASE EOF key.
    /// </summary>
    EraseEof,

    /// <summary>
    /// The PLAY key.
    /// </summary>
    Play,

    /// <summary>
    /// The ZOOM key.
    /// </summary>
    Zoom,

    /// <summary>
    /// The PA1 key.
    /// </summary>
    Pa1,

    /// <summary>
    /// The CLEAR key.
    /// </summary>
    OemClear,
}
