using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject[] storage = new GameObject[4];
    void Start()
    {
        PopulateInventory();
        OrganizeInventory();
    }

    void PopulateInventory()
    {
        storage[0] = new GameObject("Bananna");
        storage[1] = new GameObject("Apples");
        storage[2] = new GameObject("Ak-47");
    }


    void OrganizeInventory()
    {
        GameObject temp;
        for (int i = 0; i < storage.Length; i++)
        {
            temp = TakeFromInventory(i);

            if (temp.name.Length > TakeFromInventory(i + 1).name.Length)
            {
                RemoveFromInventory(i);
                PlaceInInventory(i, TakeFromInventory(i + 1));
                RemoveFromInventory(i + 1);
                PlaceInInventory(i + 1, temp);

            }
        }
    }
    public GameObject TakeFromInventory(int index)
    {
        return storage[index];
    }
    public void RemoveFromInventory(int index)
    {
        storage[index] = null;
    }
    public void PlaceInInventory(int index, GameObject obj)
    {
        storage[index] = obj;
    }
}
