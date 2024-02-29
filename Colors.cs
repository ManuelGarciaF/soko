using Raylib_cs;

namespace Game;

// TODO Replace colors with textures
static class Colors
{
    public static readonly Color Background = Color.DarkGray;
    public static readonly Color Text = Color.RayWhite;

    public static readonly Dictionary<FloorElement, Color> FloorColors = new Dictionary<FloorElement, Color>()
    {
        [FloorElement.Floor] = Color.DarkBrown,
        [FloorElement.Button] = Color.Yellow,
        [FloorElement.Goal] = Color.Gold,
    };

    public static readonly Dictionary<SurfaceElement, Color> SurfaceColors = new Dictionary<SurfaceElement, Color>()
    {
        [SurfaceElement.Wall] = Color.Gray,
        [SurfaceElement.Box] = Color.Green,
        [SurfaceElement.Player] = Color.Orange
    };
}
