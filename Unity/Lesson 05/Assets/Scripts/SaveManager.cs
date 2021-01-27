using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;


    void Start()
    {
        LoadPositions();

        LoadNames();
    }

    public void SaveData()
    {
        Debug.Log("saving");

        PlayerPrefs.SetFloat("p1-pos-x", player1.transform.position.x);
        PlayerPrefs.SetFloat("p1-pos-y", player1.transform.position.y);

        PlayerPrefs.SetFloat("p2-pos-x", player2.transform.position.x);
        PlayerPrefs.SetFloat("p2-pos-y", player2.transform.position.y);


    }

    public void LoadPositions()
    {
        Debug.Log("Loading");

        Vector3 pos = Vector3.zero;

        pos.x = PlayerPrefs.GetFloat("p1-pos-x");
        pos.y = PlayerPrefs.GetFloat("p1-pos-y");
        player1.transform.position = pos;

        pos.x = PlayerPrefs.GetFloat("p2-pos-x");
        pos.y = PlayerPrefs.GetFloat("p2-pos-y");
        player2.transform.position = pos; 


    }

    public void LoadNames()
    {
        NameTagManager nameTagManager = GetComponent<NameTagManager>();

        for (int i = 0; i < nameTagManager.nameTags.Length; i++)
        {
            nameTagManager.nameTags[i].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("p" + (i + 1) + "-name");
        }
    }

}
