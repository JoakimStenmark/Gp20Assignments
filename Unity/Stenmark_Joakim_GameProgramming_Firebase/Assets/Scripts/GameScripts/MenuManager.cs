using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public CanvasGroup signUpGroup;
    public CanvasGroup lobbyPanel;
    public CanvasGroup gameSelection;

    public TextMeshProUGUI nameText;

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
        //OpenLobbyPanel();
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

    public void OpenLobbyPanel()
    {
        lobbyPanel.interactable = true;
        lobbyPanel.alpha = lobbyPanel.alpha = 1;
        lobbyPanel.blocksRaycasts = true;
        //UpdateUserName();
    }
    public void CloseLobbyPanel()
    {
        lobbyPanel.interactable = false;
        lobbyPanel.alpha = lobbyPanel.alpha = 0;
        lobbyPanel.blocksRaycasts = false;
    }

    public void GoToGame()
    {
        GetComponent<SceneHandler>().LoadLevel(1);
    }

    public void OpenGameSelection()
    {
        gameSelection.interactable = true;
        gameSelection.alpha = 1;
        gameSelection.blocksRaycasts = true;

    }
    public void CloseGameSelection()
    {
        gameSelection.interactable = false;
        gameSelection.alpha = 0;
        gameSelection.blocksRaycasts = false;
    }

    public void ShowMenuFeedback()
    {
        //show if save was successful
    }

    public void UpdateUserName()
    {
        nameText.text = "Current Name:\n" + ActiveUser.instance.currentUser.name;
    }


}
