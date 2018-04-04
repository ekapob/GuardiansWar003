using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplayControl : MonoBehaviour {

	public static CanvasGameplayControl Instance;
	public GameObject standbyCam;
	private int allPlayerInGame;
	public GameObject loadingImg;
	public int[] sidePlayer = new int[2] {0,0};
	public bool winStat = false;
	public Text countDownWave;

	// Use this for initialization
	void Start () {
		Instance = this;
		allPlayerInGame = PhotonNetwork.playerList.Length;
		standbyCam.SetActive (true);
		//scenetest
		/*if (allPlayerInGame == 2){
			NodeControlForPlayer.Instance.playerNodeK2.SetActive (true);
			P1Spawner.Instance.enabled = false;
			NodeControlForPlayer.Instance.playerNodeM1.SetActive (true);
			P4Spawner.Instance.enabled = false;
		} else if (allPlayerInGame == 3) {
		} else if (allPlayerInGame == 4) {
			NodeControlForPlayer.Instance.playerNodeK2.SetActive (true);
			NodeControlForPlayer.Instance.playerNodeM1.SetActive (true);
			NodeControlForPlayer.Instance.playerNodeK1.SetActive (true);
			NodeControlForPlayer.Instance.playerNodeM2.SetActive (true);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.connectionStateDetailed.ToString () == "Joined") {
			standbyCam.SetActive (false);
		}

		if (allPlayerInGame != PhotonNetwork.playerList.Length && !winStat) {
			PauseAndExitButton.Instance.pause = true;
			PauseAndExitButton.Instance.allButton.SetActive (true);
			PauseAndExitButton.Instance.exitButton.SetActive (false);
		}
	}


}
