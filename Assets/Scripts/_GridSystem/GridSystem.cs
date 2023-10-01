using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem
{
    /// <summary>
    /// The x-axis length of the GridSystem.
    /// </summary>
    public int Length { get; private set; }
    /// <summary>
    /// The y-axis length of the GridSystem.
    /// </summary>
    public int Width { get; private set; }
    /// <summary>
    /// Dimension of each Grid in the GridSystem.
    /// </summary>
    public int GridSize { get; private set; }

    [SerializeField] private AreaIndicatorType areaIndicatorType;
    [SerializeField] private Tilemap _visualEffectTilemap;
    [SerializeField] private Tile _testTile;
    /// <summary>
    /// The 2D array that stores all the Grids in the GridSystem.
    /// </summary>
    private Grid[,] _grids;

    public GridSystem(int length, int width, int gridSize)
    {
        Length = length;
        Width = width;
        GridSize = gridSize;
        _grids = new Grid[Length, Width];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                _grids[x, y] = new Grid(x, y);
            }
        }
    }

    public Grid GetGrid(int x, int y)
    {
        return _grids[x, y];
    }

    public GridPosition WorldToGrid(Vector2 vector2)
    {
        int x = (int)vector2.x / GridSize;
        int y = (int)vector2.y / GridSize;
        return new GridPosition(x, y);
    }

    public Vector2 GridToWorld(GridPosition gridPosition)
    {
        int x = gridPosition.x * GridSize;
        int y = gridPosition.y * GridSize;
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return $"Dimension: {Length} by {Width}.";
    }
}

[Serializable]
public class AreaIndicatorType
{
    //public Tile s0;
    //public Tile s1Top;
    //public Tile s1Left;
    //public Tile s1Right;
    //public Tile s1Bottom;
    //public Tile s2;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
    //public Tile s0;
}
