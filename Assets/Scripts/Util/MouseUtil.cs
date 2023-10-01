using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseUtil
{
    public static Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector3Int GetMouseTilemapPosition(Tilemap tilemap)
    {
        return tilemap.WorldToCell(GetMouseWorldPosition());
    }

    public static Vector2 GetVector2ToMouse(Vector2 from)
    {
        return GetMouseWorldPosition() - from;
    }
}
