using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public CanvasGroup signUpGroup;
    public CanvasGroup lobbyPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        signUpGroup.alpha = 0f;
        lobbyPanel.alpha = 0f;
        ToggleLobbyPanel();
    }
    public void OpenLoginPanel()
    {
        signUpGroup.interactable = !signUpGroup.interactable;
        signUpGroup.alpha = 0f;
        signUpGroup.blocksRaycasts = false;
    }

    public void OpenSignUpPanel()
    {
        //signUpPanel.SetActive(true);
        //loginPanel.SetActive(false);
        signUpGroup.interactable = !signUpGroup.interactable;
        signUpGroup.alpha = 1f;
        signUpGroup.blocksRaycasts = true;

    }

    public void ToggleLobbyPanel()
    {
        lobbyPanel.interactable = !lobbyPanel.interactable;
        lobbyPanel.alpha = lobbyPanel.alpha == 0f ? 1f : 0f;
        lobbyPanel.blocksRaycasts = !lobbyPanel.blocksRaycasts;



    }

    public void GoToGame()
    {
        GetComponent<SceneHandler>().LoadLevel(1);
    }

    public void SaveName(string name)
    {
        


        UserInfo newUser = new UserInfo();
        newUser.name = name;

        //FirebaseManager.Instance.SaveData
    }

}
