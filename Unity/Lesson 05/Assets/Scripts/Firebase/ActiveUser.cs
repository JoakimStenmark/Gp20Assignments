using Firebase.Auth;
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

        
    }

    public void SaveName(string newName)
    {
        currentUser.name = newName;
        string jsonString = JsonUtility.ToJson(currentUser);
        StartCoroutine(FirebaseManager.Instance.SaveData("users/" + userID, jsonString));


    }

    

}
