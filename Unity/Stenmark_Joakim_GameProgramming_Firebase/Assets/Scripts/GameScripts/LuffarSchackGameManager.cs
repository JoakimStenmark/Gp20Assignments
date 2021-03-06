﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Players
{
    Player1 = 1,
    Player2,
    Player3,
    Player4
}


public class LuffarSchackGameManager : MonoBehaviour
{

    public static LuffarSchackGameManager instance;

    public Tile Player1TileSprite;
    public Tile Player2TileSprite;
    public Players currentPlayer;
    private int[,] virtualPlayingField;
    public int fieldSize;
    private Tile currentPlayerTile;
    public bool hasWon;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        
        InitializeGame();

        if (ActiveGame.instance.gameData.currentTurn == 0)
        {
            StartNewGame();
        }
        else
        {
            GetComponent<FirebaseListener>().Subscribe();
            //ResumeGame();
        }

    }
    private void InitializeGame()
    {
        Debug.Log("Initializing Game");
        //Do allthings that dont need firebase data
        virtualPlayingField = new int[fieldSize, fieldSize];
        PlayFieldManager.instance.InitializePlayfield(fieldSize);
        
        //make sure endOfGame is off
        hasWon = false;
        UiManager.instance.HideVictoryPanel();
    }

    //TODO Maybe Only call this when both players are in the game list
    private void StartNewGame()
    {
        Debug.Log("Starting New Game");

        SetPlayerOrder();

        ActiveGame.instance.SetBoardPositions(virtualPlayingField);

        ActiveGame.instance.SaveGameData(true);

        

    }

    private void ResumeGame()
    {
        Debug.Log("Resuming from Online Data");
        //ActiveGame.instance.LoadGameData();

        if (ActiveGame.instance.gameData.winner.nr != 0)
        {
            hasWon = true;
            PlayerHasWon(ActiveGame.instance.gameData.winner.nr);

        }

        virtualPlayingField = ActiveGame.instance.GetVirtualPlayField(fieldSize);
        PlayFieldManager.instance.UpdatePlayfield(virtualPlayingField, Player1TileSprite, Player2TileSprite);
        SetPlayerOrder();

    }

    public void UpdateGame(GameData newData)
    {
        Debug.Log("Updating from Online Data");      

        ActiveGame.instance.gameData = newData;
        virtualPlayingField = ActiveGame.instance.GetVirtualPlayField(fieldSize);
        PlayFieldManager.instance.UpdatePlayfield(virtualPlayingField, Player1TileSprite, Player2TileSprite);
        if (newData.winner.nr != 0)
        {
            hasWon = true;
            UiManager.instance.ShowVictoryPanel();
            FirebaseListener.instance.Unsubscribe();
            ActiveUser.instance.RemoveGameFromList(ActiveGame.instance.gameData.gameID);

        }
        SetPlayerOrder();

    }

    private void SetPlayerOrder()
    {      
        if (ActiveGame.instance.gameData.currentTurn == 0)
        {
            //TODO randomize who starts
            ActiveGame.instance.gameData.currentTurn = Players.Player1;
        }
        currentPlayer = ActiveGame.instance.gameData.currentTurn;
        CheckPlayerControl();

        SetCurrentTurnVisuals();


    }

    public void SetPlayerOrder(GameData newData)
    {
        currentPlayer = newData.currentTurn;
        CheckPlayerControl();

        SetCurrentTurnVisuals();
    }


    private void SetNextPlayersTurn()
    {

        if (currentPlayer == Players.Player1)
        {
            currentPlayer = Players.Player2;
        }
        else if (currentPlayer == Players.Player2)
        {
            currentPlayer = Players.Player1;
        }

        ActiveGame.instance.gameData.currentTurn = currentPlayer;
        ActiveGame.instance.SaveGameData(true);
        CheckPlayerControl();

        SetCurrentTurnVisuals();

    }

    private void CheckPlayerControl()
    {
        string userToCheck = ActiveGame.instance.GetUserIDFromPlayer(currentPlayer);
        Debug.Log(userToCheck + " is in control");
        if (userToCheck == ActiveUser.instance.userID)
        {
            BoardGameInputController.OnBoardClick += TileClick;

        }
        else
        {
            BoardGameInputController.OnBoardClick -= TileClick;

        }
    }

    private void SetCurrentTurnVisuals()
    {
        if (currentPlayer == Players.Player1)
        {
            currentPlayerTile = Player1TileSprite;
            UiManager.instance.SetCurrentPlayer(ActiveGame.instance.gameData.players[0].name,
                                                Player1TileSprite.sprite);

        }
        else if (currentPlayer == Players.Player2)
        {
            currentPlayerTile = Player2TileSprite;
            if (ActiveGame.instance.gameData.players.Count > 1)
            {
                UiManager.instance.SetCurrentPlayer(ActiveGame.instance.gameData.players[1].name,
                                                    Player2TileSprite.sprite);

            }
            else
            {
                UiManager.instance.SetCurrentPlayer("Waiting for player to join",
                                                    Player2TileSprite.sprite);
            }
        }
    }

    private void TileClick(Vector3 worldPosition)
    {

        Vector3Int selectedTile = PlayFieldManager.instance.calculateWorldToBoardPosition(worldPosition);
        if (virtualPlayingField[selectedTile.x, selectedTile.y] == 0)
        {
            virtualPlayingField[selectedTile.x, selectedTile.y] = (int)currentPlayer;
            ActiveGame.instance.SetBoardPositions(virtualPlayingField);
            PlayFieldManager.instance.ChangeTile(selectedTile, currentPlayerTile);

            hasWon = CheckWinConditions();

            if (hasWon)
            {
                Debug.Log("Won");
                PlayerHasWon(currentPlayer);
                return;
            }

            SetNextPlayersTurn();
        }
       
    }


    private bool CheckWinConditions()
    {
        int length = 5;

        //check vertical
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize - length + 1; y++)
            {
                int count = 0;
                for (int i = 0; i < length; i++)
                {

                    if (virtualPlayingField[x, y + i] == (int)currentPlayer)
                    {
                        count++;
                    
                    }

                    if (count == length)
                    {
                        return true;
                    }

                }

            }
        }

        //check Horizontal
        for (int x = 0; x < fieldSize - length + 1; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                int count = 0;
                for (int i = 0; i < length; i++)
                {

                    if (virtualPlayingField[x + i, y] == (int)currentPlayer)
                    {
                        count++;

                    }

                    if (count == length)
                    {
                        return true;
                    }

                }

            }
        }

        //check diagonal Down

        for (int x = 0; x < fieldSize - length + 1; x++)
        {
            for (int y = length - 1; y < fieldSize; y++)
            {

                int count = 0;
                for (int i = 0; i < length; i++)
                {

                    if (virtualPlayingField[x + i, y - i] == (int)currentPlayer)
                    {
                        count++;

                    }

                    if (count == length)
                    {
                        return true;
                    }

                }

            }
        }

        //check diagonal Up

        for (int x = 0; x < fieldSize - length + 1; x++)
        {
            for (int y = 0; y < fieldSize - length + 1; y++)
            {

                int count = 0;
                for (int i = 0; i < length; i++)
                {

                    if (virtualPlayingField[x + i, y + i] == (int)currentPlayer)
                    {
                        count++;

                    }

                    if (count == length)
                    {
                        return true;
                    }

                }

            }
        }

        return false;
    }
    
    private void PlayerHasWon(Players winner)
    {
        BoardGameInputController.OnBoardClick -= TileClick;
        UiManager.instance.ShowVictoryPanel();
        FirebaseListener.instance.Unsubscribe();
        ActiveUser.instance.RemoveGameFromList(ActiveGame.instance.gameData.gameID);
        if (ActiveGame.instance.GetUserIDFromPlayer(winner) == ActiveUser.instance.userID)
        {
            ActiveUser.instance.AddVictory();
            ActiveGame.instance.SetWinner();
            

            
        }

    }
    private void OnDestroy()
    {

        BoardGameInputController.OnBoardClick -= TileClick;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitializeGame();
        }
    }

}
