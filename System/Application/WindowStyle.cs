using SFML.Window;

namespace Andromeda.System
{
    public enum WindowStyle
    {
        Fixed = Styles.Titlebar | Styles.Close,
        Resizable = Styles.Titlebar | Styles.Close | Styles.Resize,
        Borderless = Styles.None,
        Fullscreen = Styles.Fullscreen,
    }
}
