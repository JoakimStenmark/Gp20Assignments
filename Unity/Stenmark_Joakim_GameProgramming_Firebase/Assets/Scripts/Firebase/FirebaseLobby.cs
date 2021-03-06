﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class FirebaseLobby : MonoBehaviour
{

    public InputField inputGameName;
    public InputField inputUserName;

    public TextMeshProUGUI statusText;

    public GameObject buttonPrefab;
    public Transform gamesListContent;

    string userID;

    public void SaveName()
    {
        userID = ActiveUser.instance.userID;

        ActiveUser.instance.currentUser.name = inputUserName.text;
        string jsonString = JsonUtility.ToJson(ActiveUser.instance.currentUser);
        StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID,
                                                         jsonString,
                                                         MenuManager.instance.UpdateUserName));

        MenuManager.instance.OpenGameSelection();

    }

    public void CreateGame()
    {
        userID = ActiveUser.instance.userID;
        
        if (inputGameName.text == "")
        {
            Debug.Log("Need name For game");
            statusText.text = "Need name For game";

            return;
        }

        if (ActiveUser.instance.currentUser.activeGames.Count > 3)
        {
            Debug.Log("Too many games made for this User, finish up some first");
            statusText.text = "Too many games made for this User, finish up some first";

            return;
        }
        statusText.text = "Game Created, refresh gamelist to find it";

        GameData game = new GameData();
        game.players = new List<PlayerInfo>();
        game.displayName = inputGameName.text;
        AddPlayerToGame(game, Players.Player1);
        //FOR Debug purposes
        //AddPlayerToGame(game, Players.Player2);

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
        userToPlayer.userID = ActiveUser.instance.userID;
        userToPlayer.nr = playerNumber;
        game.players.Add(userToPlayer);
    }

    public void RefreshGamesList()
    {
        statusText.text = "Searching games";

        foreach (Transform child in gamesListContent)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(FirebaseManager.Instance.LoadDataMultiple("games/", AddGameToLobbyList));
    }

    public void AddGameToLobbyList(string jsonstring)
    {
        statusText.text = "";

        GameData game = JsonUtility.FromJson<GameData>(jsonstring);

        if (game.players.Count > 1)
        {
            if (!ActiveUser.instance.currentUser.activeGames.Contains(game.gameID))
            {
                return;

            }
        }

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
        string jsonString;
        if (!ActiveUser.instance.currentUser.activeGames.Contains(game.gameID))
        {
            AddPlayerToGame(game, Players.Player2);

            Debug.Log("Adding game to users list");
            ActiveUser.instance.currentUser.activeGames.Add(game.gameID);
            jsonString = JsonUtility.ToJson(ActiveUser.instance.currentUser);
            StartCoroutine(FirebaseManager.Instance.SaveData("users/" + ActiveUser.instance.userID, jsonString));
        }


        ActiveGame.instance.gameData = game;
        jsonString = JsonUtility.ToJson(game);

        StartCoroutine(FirebaseManager.Instance.SaveData("games/" + game.gameID,
                                                         jsonString,
                                                         MenuManager.instance.GoToGame));

        //Kanske gör ActiveGame.instance.SaveGameData();
        


    }





}
