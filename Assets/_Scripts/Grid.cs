using System;

public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override readonly string ToString()
    {
        return "X: " + x + ", Z: " + z;
    }

    public override readonly bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }
    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}

public class Grid
{
    private GridSystem gridSystem;
    public GridPosition GridPosition { get; private set; }

    public Unit Unit { get; set; }


    public Grid(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        GridPosition = gridPosition;
    }

    public override string ToString()
    {
        if (Unit == null)
        {
            return GridPosition.ToString() + "\n" + "Empty";
        }
        else
        {
            return GridPosition.ToString() + "\n" + Unit.name;
        }
    }

    public bool HasUnit()
    {
        return Unit != null;
    }
}
