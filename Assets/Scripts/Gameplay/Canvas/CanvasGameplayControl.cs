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
	public Text playerGold;
	public GameObject shopUI;
	public GameObject unitUI;
	public Button openShopBut;
	public Button CloseShopBut;
	public Button openUnitBut;
	public Button CloseUnitBut;

	// Use this for initialization
	void Start () {
		Instance = this;
		allPlayerInGame = PhotonNetwork.playerList.Length;
		standbyCam.SetActive (true);
		playerGold.text = PlayerStats.Money.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		playerGold.text = PlayerStats.Money.ToString ();
		if (PhotonNetwork.connectionStateDetailed.ToString () == "Joined") {
			standbyCam.SetActive (false);
		}

		if (allPlayerInGame != PhotonNetwork.playerList.Length && !winStat) {
			PauseAndExitButton.Instance.pause = true;
			PauseAndExitButton.Instance.allButton.SetActive (true);
			PauseAndExitButton.Instance.exitButton.SetActive (false);
		}
	}

	public void OnClickOpenShop(){
		shopUI.SetActive (true);
		CloseShopBut.gameObject.SetActive (true);
		openShopBut.gameObject.SetActive (false);
	}

	public void OnClickCloseShop(){
		shopUI.SetActive (false);
		openShopBut.gameObject.SetActive (true);
		CloseShopBut.gameObject.SetActive (false);
	}
	public void OnClickOpenUnit(){
		unitUI.SetActive (true);
		CloseUnitBut.gameObject.SetActive (true);
		openUnitBut.gameObject.SetActive (false);
	}

	public void OnClickCloseUnit(){
		unitUI.SetActive (false);
		CloseUnitBut.gameObject.SetActive (false);
		openUnitBut.gameObject.SetActive (true);
	}
}
