using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry
{
    public const float initialTilePoint = -3.5f;
    public const float tileDistanceMultiplier = 1.5f;

    static public Vector2 PointFromGrid(Vector2Int gridPoint)
    {
        float x = (initialTilePoint + gridPoint.x) * tileDistanceMultiplier;
        float y = (initialTilePoint + gridPoint.y) * tileDistanceMultiplier;
        return new Vector2(x, y);
    }

    static public Vector2Int GridPoint(int row, int col)
    {
        return new Vector2Int(row, col);
    }

    static public Vector2Int GridFromPoint( Vector3 point)
    {
        int row = Mathf.FloorToInt(((tileDistanceMultiplier * 4) + point.x)/tileDistanceMultiplier);
        int col = Mathf.FloorToInt(((tileDistanceMultiplier * 4) + point.y)/tileDistanceMultiplier);
        return new Vector2Int(row, col);
    }

    static public bool isInsideGrid(Vector2Int gridPoint)
    {
        return (gridPoint.x <= 7 && gridPoint.y <= 7 && gridPoint.x >= 0 && gridPoint.y >= 0);
    }
}