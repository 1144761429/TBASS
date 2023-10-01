using System;

public struct GridPosition
{

    public int x;
    public int y;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static GridPosition operator +(GridPosition pos1, GridPosition pos2)
    {
        return new GridPosition(pos1.x + pos2.x, pos1.y + pos2.y);
    }

    public static GridPosition operator -(GridPosition pos1, GridPosition pos2)
    {
        return new GridPosition(pos1.x - pos2.x, pos1.y - pos2.y);
    }

    public static bool operator ==(GridPosition pos1, GridPosition pos2)
    {
        return pos1.x == pos2.x && pos1.y == pos2.y;
    }

    public static bool operator !=(GridPosition pos1, GridPosition pos2)
    {
        return pos1.x != pos2.x || pos1.y != pos2.y;
    }

    public override bool Equals(object obj)
    {
        if (obj is GridPosition)
        {
            return x == ((GridPosition)obj).x && y == ((GridPosition)obj).y;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

