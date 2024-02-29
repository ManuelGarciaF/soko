using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Game;

public class Board
{
    private GridCell[,] board;

    private int boardSize { get => board.GetLength(0); }

    public Board(GridCell[,] board)
    {
        this.board = board;
    }

    public void MovePlayers(Direction dir)
    {
        foreach (var player in FindPlayers())
        {
            MovePlayer(player, dir);
        }

        // TODO Check for win status
    }

    public void Draw(Rectangle rect)
    {
        // Calculate tile size
        int tileSize = (int)Math.Min(rect.Width / board.GetLength(0), rect.Height / board.GetLength(1));

        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                Rectangle tileRect = new Rectangle(rect.X + x * tileSize, rect.Y + y * tileSize, tileSize, tileSize);
                board[x, y].Draw(tileRect);
            }
        }
    }

    private List<Position> FindPlayers()
    {
        var list = new List<Position>();

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (board[x, y].HasPlayer())
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
        return pos.X >= 0 && pos.X < boardSize && pos.Y >= 0 && pos.Y < boardSize;
    }

    private GridCell GetCell(Position pos) => board[pos.X, pos.Y];
}

public class GridCell
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