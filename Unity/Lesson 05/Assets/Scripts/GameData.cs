using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public string displayName;
	public string gameID;	
	public List<PlayerInfo> players;
	public Vector3Int boardState;
	public Players currentTurn;
	public Players winner;
}
