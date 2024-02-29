namespace Game;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public struct Position
{
    public int X;
    public int Y;

    public Position AddDisplacement(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return new Position { X = X, Y = Y - 1 };
            case Direction.Down:
                return new Position { X = X, Y = Y + 1 };
            case Direction.Left:
                return new Position { X = X - 1, Y = Y };
            case Direction.Right:
                return new Position { X = X + 1, Y = Y };
            default:
                throw new ArgumentException("Invalid direction");
        }
    }

}
