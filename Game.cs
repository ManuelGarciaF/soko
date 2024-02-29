using static Raylib_cs.Raylib;
using static Raylib_cs.ConfigFlags;
using Raylib_cs;

namespace Game;

static class Game
{
    const int WindowHeight = 800;
    const int WindowWidth = 800;
    const int WindowMargin = 20;
    const string WindowTitle = "Soko";
    const int FontSize = 24;


    private static Board? board;
    private static int currentLevel = 1;

    public static void Main()
    {
        InitWindow(WindowWidth, WindowHeight, WindowTitle);
        SetTargetFPS(60);

        ClearWindowState(ResizableWindow);

        // Load level 1.
        board = Levels.LoadLevel(currentLevel);

        while (!WindowShouldClose())
        {
            UpdateDrawFrame();
        }
        CloseWindow();
    }


    private static void UpdateDrawFrame()
    {
        Rectangle boardRect = GetCenteredBoardRect();

        HandleKeyboard();

        BeginDrawing();

        ClearBackground(Colors.Background);

        board?.Draw(boardRect);

        DrawText($"Level {currentLevel}", 5, 5, FontSize, Color.RayWhite);

        // Show info on controls during first level.
        if (currentLevel == 1)
            DrawControls();

        EndDrawing();
    }

    private static void HandleKeyboard()
    {
        if (IsKeyPressed(KeyboardKey.Left) || IsKeyPressed(KeyboardKey.A)) board?.MovePlayers(Direction.Left);
        if (IsKeyPressed(KeyboardKey.Right) || IsKeyPressed(KeyboardKey.D)) board?.MovePlayers(Direction.Right);
        if (IsKeyPressed(KeyboardKey.Up) || IsKeyPressed(KeyboardKey.W)) board?.MovePlayers(Direction.Up);
        if (IsKeyPressed(KeyboardKey.Down) || IsKeyPressed(KeyboardKey.S)) board?.MovePlayers(Direction.Down);

        if (IsKeyPressed(KeyboardKey.R))
        {
            board = Levels.LoadLevel(currentLevel);
        }

        // if (IsKeyPressed(KeyboardKey.Z)) ; // TODO Undo

    }

    private static void DrawControls()
    {
        DrawText("WASD/Arrow Keys to move, R to restart, ESC to exit",
                 5,
                 WindowHeight - 5 - FontSize,
                 FontSize,
                 Color.RayWhite);
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
