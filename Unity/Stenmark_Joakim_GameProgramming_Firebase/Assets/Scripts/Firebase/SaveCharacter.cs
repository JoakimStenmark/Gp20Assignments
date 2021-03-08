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

    public InputField inputGameName;
    public UserInfo user;

    public void GetUserInfo()
    {
        //TODO load userInfo on login
    }

    public void SaveName(string userName)
    {

        //TODO hitta ett sätt att ladda ned user först och sedan ändra/skapa namn
        UserInfo newUser = new UserInfo();
        newUser.name = userName;

        string jsonString = JsonUtility.ToJson(newUser);
        //StartCoroutine(TrySaveToFirebase(jsonString));
        string path = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        StartCoroutine(FirebaseManager.Instance.SaveData(path, jsonString));

    }

    public void CreateGame()
    {
        if (inputGameName.text == "")
        {
            Debug.Log("ERROR");
            return;
        }
        GameData game = new GameData();
        game.displayName = inputGameName.text;
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
