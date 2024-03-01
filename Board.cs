using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Game;

public class Board : ICloneable
{
    private GridCell[,] grid;

    public Board(GridCell[,] grid)
    {
        this.grid = grid;
    }

    public void MovePlayers(Direction dir)
    {
        foreach (var player in FindPlayers()) // Would not work properly for multiple players
            MovePlayer(player, dir);
    }

    public void Draw(Rectangle rect)
    {
        // Calculate tile size
        int tileSize = (int)Math.Min(rect.Width / grid.GetLength(0), rect.Height / grid.GetLength(1));

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Rectangle tileRect = new Rectangle(rect.X + x * tileSize, rect.Y + y * tileSize, tileSize, tileSize);
                grid[x, y].Draw(tileRect);
            }
        }
    }

    private List<Position> FindPlayers()
    {
        var list = new List<Position>();

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y].HasPlayer())
                {
                    list.Add(new Position { X = x, Y = y });
                }
            }
        }

        return list;
    }

    private void MovePlayer(Position playerPos, Direction dir)
    {
        Position nextPos = playerPos.AddDisplacement(dir);
        GridCell targetCell = GetCell(nextPos);

        if (!IsInsideBoard(nextPos) || targetCell.HasWall()) return; // Don't move

        if (targetCell.HasBox())
        {
            if (!CanPush(nextPos, dir)) return; // Don't move

            MoveSurfaceElement(nextPos, dir); // Move box and continue to move the player.
        }

        // Move the player.
        MoveSurfaceElement(playerPos, dir);
    }

    private bool CanPush(Position pos, Direction dir)
    {
        Position nextPos = pos.AddDisplacement(dir);
        GridCell targetCell = GetCell(nextPos);

        // No multipush :)
        if (targetCell.HasWall() || targetCell.HasBox()) return false;

        return true;

    }

    private void MoveSurfaceElement(Position pos, Direction dir)
    {
        Position nextPos = pos.AddDisplacement(dir);
        GridCell sourceCell = GetCell(pos);
        GridCell targetCell = GetCell(nextPos);

        targetCell.surfaceElement = sourceCell.surfaceElement;
        sourceCell.surfaceElement = null;
    }

    private bool IsInsideBoard(Position pos)
    {
        return pos.X >= 0 && pos.X < grid.GetLength(0) && pos.Y >= 0 && pos.Y < grid.GetLength(1);
    }

    private GridCell GetCell(Position pos) => grid[pos.X, pos.Y];

    public object Clone()
    {
        var newGrid = new GridCell[grid.GetLength(0), grid.GetLength(1)];
        for (int x = 0; x < grid.GetLength(0); x++) // Deep copy the 2D array
            for (int y = 0; y < grid.GetLength(1); y++)
                newGrid[x, y] = (GridCell)grid[x, y].Clone();

        return new Board(newGrid);
    }
}

public class GridCell : ICloneable
{
    public FloorElement? floorElement { get; }
    public SurfaceElement? surfaceElement { get; set; }

    public GridCell(FloorElement? floorElement, SurfaceElement? surfaceElement)
    {
        this.floorElement = floorElement;
        this.surfaceElement = surfaceElement;
    }

    public void Draw(Rectangle rect)
    {
        // Draw floor element
        if (floorElement is not null)
            DrawRectangleRec(rect, Colors.FloorColors[floorElement.Value]);

        // Draw surface element above it
        if (surfaceElement is not null)
            DrawRectangleRec(rect, Colors.SurfaceColors[surfaceElement.Value]);
    }

    public bool HasPlayer() => surfaceElement == SurfaceElement.Player;
    public bool HasWall() => surfaceElement == SurfaceElement.Wall;
    public bool HasBox() => surfaceElement == SurfaceElement.Box;

    public object Clone()
    {
        return new GridCell(this.floorElement, this.surfaceElement);
    }
}

public enum FloorElement
{
    Floor,
    Button,
    Goal,
}

public enum SurfaceElement
{
    Wall,
    Box,
    Player
}
