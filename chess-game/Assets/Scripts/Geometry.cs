using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry
{
    static public Vector2 PointFromGrid(Vector2Int gridPoint)
    {
        float x = (-3.5f + gridPoint.x) * 1.5f;
        float y = (-3.5f + gridPoint.y) * 1.5f;
        return new Vector2(x, y);
    }

    static public Vector2Int GridPoint(int row, int col)
    {
        return new Vector2Int(row, col);
    }

    static public Vector2Int GridFromPoint(Vector3 point)
    {
        int col = Mathf.FloorToInt(4.0f + point.x);
        int row = Mathf.FloorToInt(4.0f + point.z);
        return new Vector2Int(row, col);
    }
}