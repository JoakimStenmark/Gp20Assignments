using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLuffarschack : MonoBehaviour
{

    string valuePath;
    
    

    //The thing we want to listen to, when it changes, HandleValueChanged will run.
    public void Subscribe()
    {
        valuePath = "games/" + ActiveGame.instance.gameData.gameID;
        Debug.Log("subscribing to value: " + valuePath);
        
        FirebaseDatabase.DefaultInstance.GetReference(valuePath).ValueChanged += HandleValueChanged;
    }
    public void Subscribe(string path)
    {
        Debug.Log("subscribing to value: " + path);
        valuePath = path;
        FirebaseDatabase.DefaultInstance.GetReference(path).ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
        Debug.Log("Value has changed: " + args.Snapshot.GetRawJsonValue());
        //update our game info
        GameData updatedGame = JsonUtility.FromJson<GameData>(args.Snapshot.GetRawJsonValue());
        //run the game with the new information
        LuffarSchackGameManager.instance.UpdateGame(updatedGame);
    }

    private void OnDisable()
    {       
        FirebaseDatabase.DefaultInstance.GetReference(valuePath).ValueChanged -= HandleValueChanged;
    }
}
