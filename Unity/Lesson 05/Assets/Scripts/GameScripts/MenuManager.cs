using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject signUpPanel;
    public GameObject loginPanel;


    public void OpenLoginPanel()
    {
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OpenSignUpPanel()
    {
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

}
