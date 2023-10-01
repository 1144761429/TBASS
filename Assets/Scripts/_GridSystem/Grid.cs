public class Grid
{
    public int X { get { return _gridPosition.x; } }
    public int Y { get { return _gridPosition.y; } }

    private GridPosition _gridPosition;

    public Grid(int x, int y)
    {
        _gridPosition = new GridPosition(x, y);
    }

    public override string ToString()
    {
        return _gridPosition.ToString();
    }
}

