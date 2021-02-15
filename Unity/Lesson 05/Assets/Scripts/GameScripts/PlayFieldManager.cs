using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;


public class PlayFieldManager : MonoBehaviour
{
    Tilemap playField;
    public Tile playfieldTile;
    public int size;
    public GameObject numberTagPrefab;
    

    void Start()
    {
        playField = GetComponent<Tilemap>();
        int tileCount = 0;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector3Int position = new Vector3Int(-y + size / 2, -x + size / 2, 0);

                playField.SetTile(position, playfieldTile);

                Transform parent = GameObject.Find("Canvas").transform;
                GameObject numberTag = Instantiate(numberTagPrefab, parent);
                Vector3 TagPosition = playField.CellToWorld(position);
                numberTag.transform.position = Camera.main.WorldToScreenPoint(TagPosition);
                numberTag.GetComponent<TextMeshProUGUI>().text = tileCount.ToString();
                tileCount++;
            }
        }
    }


}
