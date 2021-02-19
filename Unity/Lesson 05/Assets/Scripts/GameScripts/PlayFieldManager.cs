using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayFieldManager : MonoBehaviour
{
    Tilemap playField;
    public Tile playfieldTile;
    private int size;
    public GameObject numberTagPrefab;
    public static PlayFieldManager instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playField = GetComponent<Tilemap>();
        
        

    }


    public void SetupPlayField(int fieldSize)
    {
        
        for (int y = 0; y < fieldSize; y++)
        {
            for (int x = 0; x < fieldSize; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                playField.SetTile(position, playfieldTile);
                if (y == 0)
                {
                    CreateNumberTag(playField.CellToWorld(position), x, new Vector3(0.5f, -0.5f));

                }

                if (x == 0)
                {
                    CreateNumberTag(playField.CellToWorld(position), y, new Vector3(-0.5f, 0.5f));

                }

            }

        }
    }



    public Vector3Int calculateWorldToBoardPosition(Vector3 worldPosition)
    {
        return playField.WorldToCell(worldPosition);
    }

    void CreateNumberTag(Vector3 WorldPosition, int number, Vector3 offset)
    {
        Transform parent = GameObject.Find("Canvas").transform;
        GameObject numberTag = Instantiate(numberTagPrefab, parent);
        Vector3 TagPosition = WorldPosition + offset;
        numberTag.transform.position = Camera.main.WorldToScreenPoint(TagPosition);
        numberTag.GetComponent<TextMeshProUGUI>().text = number.ToString();

    }

    internal void ChangeTile(Vector3Int selectedTile, Tile newTileSprite)
    {
        playField.SetTile(selectedTile, newTileSprite);
    }


}
