using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MapGenerator
{

    public int amountOfRooms;

    public int roomMinHeight;
    public int roomMaxHeight;
    public int roomMinWidth;
    public int roomMaxWidth;

    public int corridorWidth;

    internal override void SetupRandomMap()
    {
        Vector3Int pathStartPoint = startPos;
        randomMap = new int[width, height];
        


        for (int i = 0; i < amountOfRooms; i++)
        {





        }
    }

    private void GenerateRoom(Vector3Int startPos, int roomWidth, int roomHeight)
    {
        int x = startPos.x;
        int y = startPos.y;

        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                var newPos = new Vector3Int();
                newPos.x = x + i - roomWidth / 2;
                newPos.y = y + j - roomHeight / 2;
                newPos = ClampVector(newPos);

                
                randomMap[newPos.x, newPos.y] = 1;
            }
        }

    }

    internal Vector3Int ClampVector(Vector3Int value, Vector2Int limit = new Vector2Int())
    {
        return new Vector3Int(Mathf.Clamp(value.x, limit.x, width - limit.x - 1),
            Mathf.Clamp(value.y, limit.y, height - limit.y - 1), 0);
    }
}
