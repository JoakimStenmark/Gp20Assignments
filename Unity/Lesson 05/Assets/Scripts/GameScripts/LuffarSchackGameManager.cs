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

    
   
    string tempName = "Waiting for opponent";

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
     
    }

    private void InitializeGame()
    {
        virtualPlayingField = new int[fieldSize, fieldSize];
        PlayFieldManager.instance.SetupPlayField(fieldSize);
        hasWon = false;
        SetCurrentPlayer();
        UiManager.instance.HideVictoryPanel();
        SetCurrentTurn();
    }

    private void SetCurrentTurn()
    {


        BoardGameInputController.OnBoardClick += TileClick;
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

    private void SetCurrentPlayer()
    {
        //TODO hook up players to firebase accounts
        if (ActiveGame.instance.gameData.currentTurn == null)
        {
            ActiveGame.instance.gameData.currentTurn = ActiveGame.instance.gameData.players[0];
        }



        currentPlayer = Players.Player1;
        UiManager.instance.SetCurrentPlayer(tempName, Player1TileSprite.sprite);
        currentPlayerTile = Player1TileSprite;
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

            SetNextPlayer();
        }
       
    }

    private void PlayerHasWon()
    {
        BoardGameInputController.OnBoardClick -= TileClick;
        UiManager.instance.ShowVictoryPanel();
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

    private void SetNextPlayer()
    {

        if (currentPlayer == Players.Player1)
        {
            currentPlayerTile = Player2TileSprite;
            UiManager.instance.SetCurrentPlayer(tempName + "2", Player2TileSprite.sprite);
            currentPlayer = Players.Player2;
        }
        else if (currentPlayer == Players.Player2)
        {
            currentPlayerTile = Player1TileSprite;
            UiManager.instance.SetCurrentPlayer(tempName, Player1TileSprite.sprite);
            currentPlayer = Players.Player1;
        }

        
    }
}
