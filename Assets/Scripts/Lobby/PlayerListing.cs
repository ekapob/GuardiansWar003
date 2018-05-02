﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerListing : MonoBehaviour {

	public PhotonPlayer PhotonPlayer{ get; private set;}
	[SerializeField]
	private Text _playerName;
	private Text PlayerName
	{
		get{ return _playerName; }
	}

	[SerializeField]
	private Text _playerPing;
	private Text m_playerPing
	{
		get{ return _playerPing; }
	}


	public void ApplyPhotonPlayer(PhotonPlayer photonPlayer){
		PhotonPlayer = photonPlayer;
		PlayerName.text = photonPlayer.NickName;
		/*if (PhotonNetwork.isMasterClient) {//
			PlayerName.text = MotherScript.Instance.inGameName;
		}
		PlayerName.text = MotherScript.Instance.inGameName;*/
		StartCoroutine (C_ShowPing ());
	}


	private IEnumerator C_ShowPing(){
		while(PhotonNetwork.connected){
			int ping = (int)PhotonPlayer.CustomProperties ["Ping"];
			m_playerPing.text = ping.ToString ();

			yield return new WaitForSeconds (1f);
		}

		yield break;
	}
}