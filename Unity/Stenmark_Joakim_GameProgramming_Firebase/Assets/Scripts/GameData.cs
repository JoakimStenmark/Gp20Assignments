using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public string displayName;
	public string gameID;	
	public List<PlayerInfo> players;
	
	//CHANGE TO VECTOR2List. 2D array not supported :(
	public List<Vector2Int> p1BoardPositions;
	public List<Vector2Int> p2BoardPositions;
	
	public Players currentTurn;
	public PlayerInfo winner;

}
