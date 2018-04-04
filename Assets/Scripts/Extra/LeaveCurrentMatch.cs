using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCurrentMatch : MonoBehaviour {
	
	public void OnClick_LeaveMatch(){
		Time.timeScale = 1f;
		PhotonNetwork.LeaveRoom ();
		PlayerNetwork.Instance.joinRoomNum = 0;
		PlayerNetwork.Instance.PlayersInGame = 0;
		MotherScript.Instance.currentGameMode = 0;
		LobbyChat.Instance.chatClient.Disconnect ();
		PhotonNetwork.LoadLevel (2);
	}

}
