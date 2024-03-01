using Raylib_cs;

namespace Game;

// TODO Replace colors with textures
static class Colors
{
    public static readonly Color Background = Color.DarkGray;
    public static readonly Color Text = Color.RayWhite;

    public static readonly Dictionary<FloorObject, Color> FloorColors = new Dictionary<FloorObject, Color>()
    {
        [FloorObject.Floor] = Color.DarkBrown,
        [FloorObject.Button] = Color.Yellow,
        [FloorObject.Goal] = Color.Gold,
    };

    public static readonly Dictionary<SurfaceObject, Color> SurfaceColors = new Dictionary<SurfaceObject, Color>()
    {
        [SurfaceObject.Wall] = Color.Gray,
        [SurfaceObject.Box] = Color.Green,
        [SurfaceObject.Player] = Color.Orange
    };
}
