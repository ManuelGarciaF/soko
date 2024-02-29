using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Game;

static class Game
{
    const int WindowHeight = 600;
    const int WindowWidth = 800;
    const int WindowMargin = 20;
    const string WindowTitle = "Soko";

    private static Board? board;

    public static void Main()
    {
        InitWindow(WindowWidth, WindowHeight, WindowTitle);
        SetTargetFPS(1);

        Rectangle boardRect = GetCenteredBoardRect();

        // board = Levels.Level1;

        while (!WindowShouldClose())
        {
            DrawFrame(boardRect);
        }

        CloseWindow();
    }

    private static void DrawFrame(Rectangle boardRect)
    {
        BeginDrawing();
        ClearBackground(Colors.Background);

        board?.Draw(boardRect);

        EndDrawing();
    }

    private static Rectangle GetCenteredBoardRect()
    {
        int availableWidth = WindowWidth - 2 * WindowMargin;
        int availableHeight = WindowHeight - 2 * WindowMargin;
        int boardSize = Math.Min(availableWidth, availableHeight);

        int x = (WindowWidth - boardSize) / 2;
        int y = (WindowHeight - boardSize) / 2;

        return new Rectangle(x, y, boardSize, boardSize);
    }
}

public class Board
{
    private GridCell[,] board;

    public Board(GridCell[,] board)
    {
        this.board = board;
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
}

public class GridCell
{
    FloorElement floorElement = FloorElement.Floor; // Default value
    SurfaceElement? surfaceElement;

    public void Draw(Rectangle rect)
    {
        // Draw floor element
        DrawRectangleRec(rect, Colors.FloorColors[floorElement]);

        // Draw surface element
        if (surfaceElement is not null)
            DrawRectangleRec(rect, Colors.SurfaceColors[surfaceElement.Value]);
    }
}

public enum FloorElement
{
    Floor = 0,
    Target,
    PlayerGoal,
}

public enum SurfaceElement
{
    Wall,
    Box,
    Player
}
