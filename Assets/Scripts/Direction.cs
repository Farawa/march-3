using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    //sides
    public static Vector2Int GetBottom(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x, currentPosition.y + 1);
    }
    public static Vector2Int GetTop(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x, currentPosition.y - 1);
    }
    public static Vector2Int GetLeft(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x - 1, currentPosition.y);
    }
    public static Vector2Int GetRight(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x + 1, currentPosition.y);
    }
    //angular
    public static Vector2Int GetBottomLeft(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x - 1, currentPosition.y+1);
    }
    public static Vector2Int GetBottomRight(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x + 1, currentPosition.y + 1);
    }
    public static Vector2Int GetTopLeft(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x - 1, currentPosition.y - 1);
    }
    public static Vector2Int GetTopRight(Vector2Int currentPosition)
    {
        return new Vector2Int(currentPosition.x + 1, currentPosition.y - 1);
    }
    //custom
    public static Vector2Int GetPosition(Vector2Int currentPosition, int x, int y)
    {
        return new Vector2Int(currentPosition.x + x, currentPosition.y - y);
    }
}
