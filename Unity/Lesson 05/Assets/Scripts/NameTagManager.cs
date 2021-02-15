using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NameTagManager : MonoBehaviour
{
    public GameObject nameTagPrefab;
    public Vector3 offset;
    public GameObject[] cars;

    public GameObject[] nameTags;

    void Awake()
    {
        SpawnNameTags();

    }

    public void SpawnNameTags()
    {
        nameTags = new GameObject[cars.Length];

        Transform parent = GameObject.Find("Canvas").transform;
        for (int i = 0; i < cars.Length; i++)
        {
            nameTags[i] = Instantiate(nameTagPrefab, parent);
        }
    }

    void LateUpdate()
    {
        
        for (int i = 0; i < cars.Length; i++)
        {
            nameTags[i].transform.position = Camera.main.WorldToScreenPoint(cars[i].transform.position + offset);
        }
    }
}
