
using UnityEngine;
using Firebase.Auth;
using System;

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

    public string GetUserIDFromPlayer(Players player)
    {
        PlayerInfo foundPlayer = gameData.players.Find(x => x.nr == player);
        if (foundPlayer != null)
        {
            return foundPlayer.userID;
        }
        else
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
        StartCoroutine(FirebaseManager.Instance.SaveData("games/" + gameData.gameID, jsonString));
    }
}
