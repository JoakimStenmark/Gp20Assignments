using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this to any gameobject, and the test should be run when you play
/// If any problems just ask Jesper Uddefors!
/// </summary>
public class Inventory : MonoBehaviour
{

    public GameObject[] storage = new GameObject[5];

    #region Test
    //No need to change this function
    void Start()
    {
        PopulateInventory();
        OrganizeInventory();

        if (IsCorrectlyOrganized())
        {
            Debug.Log("You did it!!");
        }
        else
        {
            Debug.LogError("Something is still wrong");
        }
    }
    #endregion


    // Feel free to change these function, so it gives no errors and passes the Test. 
    // It still should have the same structure 2 for loops
    void OrganizeInventory()
    {
        GameObject temp;
        for (int i = 0; i < storage.Length; i++)
        {
            for (int j = 1; j < storage.Length; j++)
            {
                temp = TakeFromInventory(i);
                if (Compare(temp.name, TakeFromInventory(j).name))
                {
                    RemoveFromInventory(i);
                    PlaceInInventory(i, TakeFromInventory(j));
                    PlaceInInventory(j, temp);
                }

                //j--;
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

    #region DO NOT TOUCH!!!
    // Minimize this function as it works as intended
    /// <summary>
    /// Checks if <paramref name="name1"/> is alphabetecly before(false) or after(true) <paramref name="name2"/> 
    /// </summary>
    /// <param name="name1"></param>
    /// <param name="name2"></param>
    /// <returns></returns>
    bool Compare(string name1, string name2)
    {
        if (name1.Length < 1)
            return false;

        if (name2.Length < 1)
            return true;

        for (int i = 0; i < name1.Length; i++)
        {
            if (i > name2.Length -1)
                return true;

            if (name1[i] == name2[i])
                continue;

            if (name1[i] < name2[i])
                return true;
            else
                return false;
        }
        return false;
    }


    /// <summary>
    /// This Function should not be changed. Used for testing.
    /// </summary>
    void PopulateInventory()
    {
        storage = new GameObject[5];
        storage[0] = new GameObject("Egg");
        storage[1] = new GameObject("Bananna");
        storage[3] = new GameObject("Sallad");
        storage[4] = new GameObject("Banjo");
    }
    /// <summary>
    /// In this function you can find the order it should be in
    /// </summary>
    /// <returns></returns>
    bool IsCorrectlyOrganized()
    {
        string[] expectedOrder = new string[5];
        expectedOrder[0] = "Bananna";
        expectedOrder[1] = "Banjo";
        expectedOrder[2] = "Egg";
        expectedOrder[3] = "Sallad";
        expectedOrder[4] = "";

        for (int i = 0; i < storage.Length; i++)
        {
            if ((storage[i]?.name ?? "") != expectedOrder[i])
                return false;
        }
        return true;
    }
    #endregion
}
