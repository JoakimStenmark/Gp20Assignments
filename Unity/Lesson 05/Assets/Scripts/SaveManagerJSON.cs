using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using System.Net;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

[Serializable]
public class MultiplePlayers
{
    public PlayerInfo[] players;
}

[Serializable]
public class PlayerInfo
{
    public string Name;
    public Vector3 Position;
}

public class SaveManagerJSON : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;


    void Start()
    {
        //LoadData();
        //LoadFromFirebase();

        //SaveData();
        //LoadNames();
        Debug.Log(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
    }

    public void SaveData()
    {

        Debug.Log("saving");

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        MultiplePlayers multiplePlayers = new MultiplePlayers();
        multiplePlayers.players = new PlayerInfo[players.Length];

        for (int i = 0; i < multiplePlayers.players.Length; i++)
        {
            multiplePlayers.players[i] = new PlayerInfo();
            multiplePlayers.players[i].Position = players[i].transform.position;
            multiplePlayers.players[i].Name = players[i].name;

        }

        string jsonString = JsonUtility.ToJson(multiplePlayers);

        //PlayerPrefs.SetString("JsonTesting", jsonString);
        SaveToFile("jockesjson", jsonString);
        //SaveOnline("jockesjson", jsonString);
        SaveToFirebase(jsonString);
    }

    private void SaveToFirebase(string jsonString)
    {
        Debug.Log("Trying to write data");
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
        var dataTask = db.RootReference.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetRawJsonValueAsync(jsonString);
    }

    public void LoadFromFirebase()
    {
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            DataSnapshot snap = task.Result;

            LoadState(snap.GetRawJsonValue());
        });
    }

    private void LoadState(string jsonString)
    {
        MultiplePlayers multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].name = multiplePlayers.players[i].Name;
            players[i].transform.position = multiplePlayers.players[i].Position;

        }

        NameTagManager nameTagManager = GetComponent<NameTagManager>();

        for (int i = 0; i < nameTagManager.nameTags.Length; i++)
        {
            nameTagManager.nameTags[i].GetComponent<TextMeshProUGUI>().text = multiplePlayers.players[i].Name;
        }
    }

    public void SaveToFile(string fileName, string jsonString)
    {
        // Open a file in write mode. This will create the file if it's missing.
        // It is assumed that the path already exists.
        using (FileStream stream = File.OpenWrite(Application.persistentDataPath + "\\" + fileName))
        {
            // Truncate the file if it exists (we want to overwrite the file)
            stream.SetLength(0);

            // Convert the string into bytes. Assume that the character-encoding is UTF8.
            // Do you not know what encoding you have? Then you have UTF-8
            var bytes = Encoding.UTF8.GetBytes(jsonString);

            // Write the bytes to the hard-drive
            stream.Write(bytes, 0, bytes.Length);

            // The "using" statement will automatically close the stream after we leave
            // the scope - this is VERY important
        }
    }

    public void LoadData()
    {
        Debug.Log("Loading");

        //string jsonString = PlayerPrefs.GetString("JsonTesting");
        
        string jsonString2 = Load("jockesjson");

        //string jsonString = LoadOnline("jockesjson");

        //if (jsonString != jsonString2)
        //{
        //    Debug.LogError("json saves are not the same");
        //}

        MultiplePlayers multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString2);

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].name = multiplePlayers.players[i].Name;
            players[i].transform.position = multiplePlayers.players[i].Position;

        }

        NameTagManager nameTagManager = GetComponent<NameTagManager>();

        for (int i = 0; i < nameTagManager.nameTags.Length; i++)
        {
            nameTagManager.nameTags[i].GetComponent<TextMeshProUGUI>().text = multiplePlayers.players[i].Name;
        }

    }

    public string Load(string fileName)
    {
        // Open a stream for the supplied file name as a text file
        using (var stream = File.OpenText(Application.persistentDataPath + "\\" + fileName))
        {
            // Read the entire file and return the result. This assumes that we've written the
            // file in UTF-8
            return stream.ReadToEnd();
        }
    }

    //Saves the playerInfo string on the server.

    public String LoadOnline(string name)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + name);
        var response = (HttpWebResponse)request.GetResponse();

        // Open a stream to the server so we can read the response data it sent back from our GET request
        using (var stream = response.GetResponseStream())
        {
            using (var reader = new StreamReader(stream))
            {
                // Read the entire body as a string
                var body = reader.ReadToEnd();

                return body;
            }
        }
    }
    public void SaveOnline(string fileName, string saveData)
    {
        //url
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + fileName);
        request.ContentType = "application/json";
        request.Method = "PUT";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(saveData);
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Debug.Log(result);
        }
    }

    public void LoadNames()
    {

    }
}
