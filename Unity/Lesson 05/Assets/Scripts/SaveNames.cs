using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveNames : MonoBehaviour
{
    public InputField name1;
    public InputField name2;


    void Start()
    {
        name1.text = PlayerPrefs.GetString("p1-name");
        name2.text = PlayerPrefs.GetString("p2-name");

        
    }

    public void SavePlayerNames()
    {

        Debug.Log("saving");
        PlayerPrefs.SetString("p1-name", name1.text);
        PlayerPrefs.SetString("p2-name", name2.text);


    }
}
