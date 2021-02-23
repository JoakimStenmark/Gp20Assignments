using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public TextMeshProUGUI currentPlayer;
    public Image currentPlayerImage;
    public CanvasGroup victoryPanel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetCurrentPlayer(string playerName, Sprite sprite)
    {
        currentPlayer.text = playerName;
        currentPlayerImage.sprite = sprite;
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.alpha = 1;
        victoryPanel.blocksRaycasts = true;
        victoryPanel.interactable = true;
    }

    public void HideVictoryPanel()
    {
        victoryPanel.alpha = 0;
        victoryPanel.blocksRaycasts = false;
        victoryPanel.interactable = false;
    }

    public void GoBackToLobby()
    {
        //TODO check if there are problems being able to go back whenever
        GetComponent<SceneHandler>().LoadLevel(0);
    }
}
