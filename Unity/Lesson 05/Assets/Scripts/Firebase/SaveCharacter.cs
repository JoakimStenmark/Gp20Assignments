using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class SaveCharacter : MonoBehaviour
{
    public InputField playerName;

    public void SaveName()
    {
        MultiplePlayers multiplePlayers = new MultiplePlayers();
        multiplePlayers.players = new PlayerInfo[2];
        multiplePlayers.players[0] = new PlayerInfo();
        multiplePlayers.players[1] = new PlayerInfo();

        multiplePlayers.players[0].Name = playerName.text;
        string jsonString = JsonUtility.ToJson(multiplePlayers);
        StartCoroutine(TrySaveToFirebase(jsonString));
    }

    private void SaveToFirebase(string jsonString)
    {
        Debug.Log("Trying to write data to user: " + FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
        var dataTask = db.RootReference.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetRawJsonValueAsync(jsonString);
    }

    private IEnumerator TrySaveToFirebase(string jsonString)
    {
        Debug.Log("Trying to write data to user: " + FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
        var dataTask = db.RootReference.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetRawJsonValueAsync(jsonString);


        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);
        else
        {
            Debug.Log("Player created");
            MenuManager.instance.GoToGame();

        }
    }

}
