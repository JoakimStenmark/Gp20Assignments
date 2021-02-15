using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuffarSchackGameManager : MonoBehaviour
{
    private int[,] playingField;
    public int fieldSize;
    


    void Start()
    {
        playingField = new int[fieldSize, fieldSize];
        int tileCount = 0;
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                
                playingField[x, y] = tileCount;
                tileCount++;

            }
        }
    }

   
}
