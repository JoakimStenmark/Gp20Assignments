using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerMapGenerator : MapGenerator
{
    Vector3Int walkerPos;
    public int steps;


    internal override void SetupRandomMap()
    {
        walkerPos = startPos;
        randomMap = new int[width, height];
        int wallsHit = 0;
        for (int i = 0; i < steps; i++)
        {

            float dice = Mathf.PerlinNoise(0.3f * i + Random.Range(0f,1f),0);
            int diceRoll = (wallsHit + Mathf.FloorToInt(dice * 5)) % 4;
            //Debug.Log(diceRoll);
            switch (diceRoll)
            {
                case 0:
                    walkerPos += Vector3Int.left;
                    break;
                case 1:
                    walkerPos += Vector3Int.right;
                    break;
                case 2:
                    walkerPos += Vector3Int.down;
                    break;
                default:
                    walkerPos += Vector3Int.up;
                    break;
            }

            if (CheckForOutBounds(walkerPos) || i % 100 == 0)
            {
                wallsHit++;
            }

            walkerPos.Clamp( Vector3Int.zero, new Vector3Int (width - 1, height - 1,0));
            randomMap[walkerPos.x, walkerPos.y] = 1;

        }
     
    }

    private bool CheckForOutBounds(Vector3Int walkerPos)
    {
        if (walkerPos.x > width || walkerPos.x < 0)
        {
            return true; 

        }

        if (walkerPos.y > height || walkerPos.y < 0)
        {
            return true;

        }

        return false;

    }
}
