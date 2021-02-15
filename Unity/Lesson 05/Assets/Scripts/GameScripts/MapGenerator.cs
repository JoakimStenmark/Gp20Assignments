using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    public int width;
    public int height;

    internal int[,] randomMap;
    internal Tilemap map;

    internal Vector3Int startPos;
    public int startAreaSize;

    public RuleTile ruleTile;

    void Start()
    {
        map = GetComponent<Tilemap>();
        Generate();
    }

    public void Generate()
    {
        ClearMap();

        startPos = new Vector3Int(width / 2, height / 2, 0);

        SetupRandomMap();

        CreateStartingPoint();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (randomMap[x, y] == 1)
                    map.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), ruleTile);
            }
        }

    }

    private void ClearMap()
    {
        map.ClearAllTiles();
    }

    internal virtual void SetupRandomMap()
    {
        randomMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                randomMap[x, y] = Random.Range(0, 2);

            }
        }
    }

    void CreateStartingPoint()
    {
        //randomMap[startPos.x - 1, startPos.y - 1] = 1;
        //randomMap[startPos.x - 1, startPos.y] = 1;
        //randomMap[startPos.x - 1, startPos.y + 1] = 1;
        //randomMap[startPos.x, startPos.y - 1] = 1;
        //randomMap[startPos.x, startPos.y] = 1;
        //randomMap[startPos.x, startPos.y + 1] = 1;
        //randomMap[startPos.x + 1, startPos.y - 1] = 1;
        //randomMap[startPos.x + 1, startPos.y] = 1;
        //randomMap[startPos.x + 1, startPos.y + 1] = 1;

        for (int x = startPos.x -startAreaSize; x < startPos.x + startAreaSize; x++)
        {
            for (int y = startPos.y - startAreaSize; y < startPos.y + startAreaSize; y++)
            {
                randomMap[x, y] = 1;
            }
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = 
            map.CellToWorld(new Vector3Int(-startPos.x + width/2 + 1, -startPos.y + height/2 + 1, 0));

    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
            Debug.Log("Generate");
        }
    }


}
