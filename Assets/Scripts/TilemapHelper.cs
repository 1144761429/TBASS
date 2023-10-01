using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHelper : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3 mouseScreenPos;
    public Vector3 mouseTilePosition;
    public Vector3 mouseWorldPositon;

    private void Update()
    {
        mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -10;
        mouseWorldPositon = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseTilePosition = tilemap.WorldToCell(mouseWorldPositon);
    }
}
