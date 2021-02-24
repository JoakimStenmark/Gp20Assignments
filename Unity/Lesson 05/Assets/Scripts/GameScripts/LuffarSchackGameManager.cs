using System;
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

        if (ActiveGame.instance.gameData.boardState != null)
        {
            ResumeGame();
        }
        else
        {
            StartNewGame();
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

        ActiveGame.instance.gameData.boardState = virtualPlayingField;

        ActiveGame.instance.SaveGameData();

    }


    private void ResumeGame()
    {
        Debug.Log("Resuming from Online Data");
        ActiveGame.instance.LoadGameData();

        if (ActiveGame.instance.gameData.winner != null)
        {
            hasWon = true;
            UiManager.instance.ShowVictoryPanel();

        }

        virtualPlayingField = ActiveGame.instance.gameData.boardState;
        PlayFieldManager.instance.UpdatePlayfield(virtualPlayingField, Player1TileSprite, Player2TileSprite);


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
        ActiveGame.instance.SaveGameData();
        CheckPlayerControl();

        SetCurrentTurnVisuals();

    }

    private void CheckPlayerControl()
    {
        if (ActiveGame.instance.GetUserIDFromPlayer(currentPlayer) == ActiveUser.instance.userID)
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
            UiManager.instance.SetCurrentPlayer(ActiveGame.instance.gameData.players[1].name,
                                                Player2TileSprite.sprite);
        }
    }

    private void TileClick(Vector3 worldPosition)
    {

        Vector3Int selectedTile = PlayFieldManager.instance.calculateWorldToBoardPosition(worldPosition);
        if (virtualPlayingField[selectedTile.x, selectedTile.y] == 0)
        {
            virtualPlayingField[selectedTile.x, selectedTile.y] = (int)currentPlayer;
            PlayFieldManager.instance.ChangeTile(selectedTile, currentPlayerTile);

            hasWon = CheckWinConditions();

            if (hasWon)
            {
                Debug.Log("Won");
                PlayerHasWon();
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
    private void PlayerHasWon()
    {
        BoardGameInputController.OnBoardClick -= TileClick;
        UiManager.instance.ShowVictoryPanel();
    }
    private void OnDisable()
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
