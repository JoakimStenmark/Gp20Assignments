using UnityEngine;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using Firebase.Database;

public class ActiveGame : MonoBehaviour
{
    public static ActiveGame instance;
    public GameData gameData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public string GetUserIDFromPlayer(Players playerToFind)
    {
        //PlayerInfo foundPlayer = gameData.players.Find(x => x.nr == player);
        //if (foundPlayer != null)
        //{
        //    return foundPlayer.userID;
        //}

        foreach (PlayerInfo player in gameData.players)
        {
            if (player.nr == playerToFind)
            {
                return player.userID;
            }
        }

        return "PlayerNotFound";

    }

    public void LoadGameData()
    {
        StartCoroutine(FirebaseManager.Instance.LoadData("games/" + gameData.gameID, UpdateGame));
    }

    private void UpdateGame(string jsonData)
    {
        if (jsonData == "" || jsonData == null)
        {
            Debug.LogError("No GameData Found");
            gameData = new GameData();
        }
        else
        {
            gameData = JsonUtility.FromJson<GameData>(jsonData);

        }
    }

    public void SaveGameData()
    {
        string jsonString = JsonUtility.ToJson(gameData);
        StartCoroutine(FirebaseManager.Instance.SaveData("games/" + gameData.gameID, jsonString, FirebaseLuffarschack.instance.Subscribe));
    }

    public int[,] GetVirtualPlayField(int fieldSize)
    {
        int[,] playField = new int[fieldSize, fieldSize];
        foreach (Vector2Int position in gameData.p1BoardPositions)
        {
            playField[position.x, position.y] = 1;
        }
        foreach (Vector2Int position in gameData.p2BoardPositions)
        {
            playField[position.x, position.y] = 2;
        }

        return playField;

    }

    public void SetBoardPositions(int[,] playField)
    {
        gameData.p1BoardPositions = new List<Vector2Int>();
        gameData.p2BoardPositions = new List<Vector2Int>();

        int fieldSize = playField.GetLength(0);
        for (int y = 0; y < fieldSize; y++)
        {
            for (int x = 0; x < fieldSize; x++)
            {
                if (playField[x, y] == 1)
                {
                    gameData.p1BoardPositions.Add(new Vector2Int(x, y));
                }
                else if (playField[x, y] == 2)
                {
                    gameData.p2BoardPositions.Add(new Vector2Int(x, y));
                }

            }

        }
    }

    public void SetWinner()
    {
        if (gameData.currentTurn == Players.Player1)
        {
            gameData.winner = gameData.players[0];

        }
        else
        {
            gameData.winner = gameData.players[1];

        }
        SaveGameData();
        //ActiveUser.instance.RemoveGameFromList(gameData.gameID);

    }


}
