using Raylib_cs;

namespace Game;

static class Colors
{
    public static readonly Color Background = Color.DarkGray;
    public static readonly Color Text = Color.RayWhite;

    public static readonly Dictionary<GridCell, Color> CellColors = new Dictionary<GridCell, Color>()
    {
        [GridCell.Wall] = Color.Gray,
        [GridCell.Floor] = Color.DarkBrown,
        [GridCell.Box] = Color.Green,
        [GridCell.Target] = Color.Yellow,
        [GridCell.PlayerGoal] = Color.Gold,
        [GridCell.Player] = Color.Orange
    };
}
