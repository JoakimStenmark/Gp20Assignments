﻿using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLuffarschack : MonoBehaviour
{
    public static FirebaseLuffarschack instance;
    string valuePath;
    public bool subscribed;

    private void Awake()
    {
        subscribed = false;

        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    //The thing we want to listen to, when it changes, HandleValueChanged will run.
    public void Subscribe()
    {
        if (subscribed)
        {
            return;
        }
        valuePath = "games/" + ActiveGame.instance.gameData.gameID;
        Debug.Log("subscribing to value: " + valuePath);
        
        FirebaseDatabase.DefaultInstance.GetReference(valuePath).ValueChanged += HandleValueChanged;
        subscribed = true;
    }

    public void Unsubscribe()
    {
        if (!subscribed)
        {
            return;
        }
        valuePath = "games/" + ActiveGame.instance.gameData.gameID;
        Debug.Log("Unsubscribing to value: " + valuePath);

        FirebaseDatabase.DefaultInstance.GetReference(valuePath).ValueChanged -= HandleValueChanged;
        subscribed = false;

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

    //private void OnDestroy()
    //{       
    //    FirebaseDatabase.DefaultInstance.GetReference(valuePath).ValueChanged -= HandleValueChanged;
    //}
}
