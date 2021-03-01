using Firebase.Auth;
using System;
using UnityEngine;

public class ActiveUser : MonoBehaviour
{
    public static ActiveUser instance;
    public UserInfo currentUser;
    public string userID;

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


    public void LoadUserData()
    {
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        StartCoroutine(FirebaseManager.Instance.LoadData("users/" + userID, LoadCurrentUser));
    }


    public void LoadCurrentUser(string jsonData)
    {
        if (jsonData == "" || jsonData == null)
        {
            currentUser = new UserInfo();
        }
        else
        {
            currentUser = JsonUtility.FromJson<UserInfo>(jsonData);
        }

        MenuManager.instance.UpdateUserName();
        
    }

    public void AddVictory()
    {
        currentUser.victories++;
        string jsonString = JsonUtility.ToJson(currentUser);
        StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID, jsonString));

    }

    public void RemoveGameFromList(string gameID)
    {
        Debug.Log("Check GameID to Remove");
        if (currentUser.activeGames.Exists(e => e == gameID))
        {
            Debug.Log("Removing gameID");

            currentUser.activeGames.Remove(gameID);
        }

        string jsonString = JsonUtility.ToJson(currentUser);
        StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID, jsonString));

    }
}
