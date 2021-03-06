﻿using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    //Singleton
    private static FirebaseManager instance;
    public static FirebaseManager Instance { get { return instance; } }

    //Delegates
    public delegate void OnLoadedDelegate(string jsonData);
    public delegate void OnSaveDelegate();

    FirebaseDatabase db;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        db = FirebaseDatabase.DefaultInstance;
    }

    public IEnumerator LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        var dataTask = db.RootReference.Child(path).GetValueAsync();
        yield return new WaitUntil(() => dataTask.IsCompleted);
        string jsonData = dataTask.Result.GetRawJsonValue();

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        onLoadedDelegate(jsonData);
    }

    public IEnumerator LoadDataMultiple(string path, OnLoadedDelegate onLoadedDelegate)
    {
        var dataTask = db.RootReference.Child(path).GetValueAsync();
        yield return new WaitUntil(() => dataTask.IsCompleted);
        string jsonData = dataTask.Result.GetRawJsonValue();

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        foreach (var item in dataTask.Result.Children)
        {
            onLoadedDelegate(item.GetRawJsonValue());
        }
    }

    public IEnumerator SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        Debug.Log("Saving");
        var dataTask = db.RootReference.Child(path).SetRawJsonValueAsync(data);
        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (onSaveDelegate != null)
        {
            onSaveDelegate();
        }
    }

    public IEnumerator CheckForGame(string path, OnLoadedDelegate onLoadedDelegate = null)
    {
        Debug.Log("checking for game");
        var dataTask = db.GetReference("games").OrderByChild("status").EqualTo("new").GetValueAsync();

        yield return new WaitUntil(() => dataTask.IsCompleted);

        string jsonData = dataTask.Result.GetRawJsonValue();

        Debug.Log("game data: " + jsonData);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (dataTask.Result.ChildrenCount > 0)
        {
            foreach (var item in dataTask.Result.Children)
            {
                Debug.Log("multiple data found: " + item.GetRawJsonValue());

                onLoadedDelegate(item.GetRawJsonValue());
                break;
            }
        }
        else
        {
            onLoadedDelegate(jsonData);
        }
    }



}
