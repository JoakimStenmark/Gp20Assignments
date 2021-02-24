using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class FirebaseLobby : MonoBehaviour
{

    public InputField inputGameName;
    

    public GameObject buttonPrefab;
    public Transform gamesListContent;

    string userID;
    private void Start()
    {
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    public void SaveName(string userName)
    {
        ActiveUser.instance.SaveName(userName);

    }

    public void CreateGame()
    {
        if (inputGameName.text == "")
        {
            Debug.Log("Need name For game");
            return;
        }

        if (ActiveUser.instance.currentUser.activeGames.Count > 3)
        {
            Debug.Log("Too many games made for this User, finish up some first");
            return;
        }

        GameData game = new GameData();
        game.players = new List<PlayerInfo>();
        game.displayName = inputGameName.text;
        AddPlayerToGame(game, Players.Player1);
        //FOR Debug purposes
        AddPlayerToGame(game, Players.Player2);

        string key = FirebaseDatabase.DefaultInstance.RootReference.Child("games/").Push().Key;
        game.gameID = key;

        string jsonString = JsonUtility.ToJson(game);
        string path = "games/" + key;
        StartCoroutine(FirebaseManager.Instance.SaveData(path, jsonString));

        ActiveUser.instance.currentUser.activeGames.Add(key);
        jsonString = JsonUtility.ToJson(ActiveUser.instance.currentUser);
        StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID, jsonString));

    }


    private void AddPlayerToGame(GameData game, Players playerNumber)
    {
        PlayerInfo userToPlayer = new PlayerInfo();
        userToPlayer.name = ActiveUser.instance.currentUser.name;
        userToPlayer.userID = userID;
        userToPlayer.nr = playerNumber;
        game.players.Add(userToPlayer);
    }

    public void RefreshGamesList()
    {
        foreach (Transform child in gamesListContent)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(FirebaseManager.Instance.LoadDataMultiple("games/", AddGameToLobbyList));
    }

    public void AddGameToLobbyList(string jsonstring)
    {
        GameData game = JsonUtility.FromJson<GameData>(jsonstring);

        Button newButton = Instantiate(buttonPrefab, gamesListContent).GetComponent<Button>();

        TextMeshProUGUI[] texts = newButton.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = game.displayName;
        texts[1].text = "Players: ";
        foreach (PlayerInfo player in game.players)
        {
            texts[1].text += player.name + ", ";
        }
        newButton.onClick.AddListener(() => JoinGame(game));
    }

    public void JoinGame(GameData game)
    {

        if (!ActiveUser.instance.currentUser.activeGames.Contains(game.gameID))
        {
            Debug.Log("Adding game to users list");

            ActiveUser.instance.currentUser.activeGames.Add(game.gameID);
            AddPlayerToGame(game, Players.Player2);
            string jsonString = JsonUtility.ToJson(ActiveUser.instance.currentUser);
            StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID, jsonString));
        }


        ActiveGame.instance.gameData = game;
        //Kanske gör ActiveGame.instance.SaveGameData();
        MenuManager.instance.GoToGame();


    }





}
