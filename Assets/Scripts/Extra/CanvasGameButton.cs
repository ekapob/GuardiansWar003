using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameButton : MonoBehaviour {

	[Header("Mode Spawner")]
	public List<GameObject> spawner1 = new List<GameObject>();
	[Header("Side Spawner")]
	public List<GameObject> spawner2 = new List<GameObject>();
	public static CanvasGameButton Instance;
	private bool showButtonState;
	[SerializeField]
	private GameObject connectCamera;
	public GameObject allButton;
	public bool pause;
	public Text clockCount;
	public float timer = 10f;
	private int timeToStr = 10;
	public int[] mode = new int[3];// 1 : Random | 2 : Ah Base is on FIRE | 3 : KilL'a BosSSS
	[SerializeField]
	private GameObject kickPlayer; 
	private int limitPlayer;
	public bool kickKeyLock;
	public int playMode;// 1 : Ah Base is on FIRE | 2 : KilL'a BosSSS
	public int playSide;// 1 : Knight | 2 : Monster
	public int[] side;
	public bool clickModeState;
	public Text modePrintTxt;
	public Button mode1But;
	public Button mode2But;
	public Button kniBut;
	public Button monBut;


	// Use this for initialization
	void Awake () {
		clickModeState = true;
		side = new int[PhotonNetwork.playerList.Length];
		clockCount.text = timeToStr.ToString ();
		pause = false;
		allButton.SetActive (false);
		Instance = this;
		showButtonState = false;
		connectCamera.SetActive (true);
		limitPlayer = PhotonNetwork.playerList.Length;
		kickPlayer.SetActive (false);
		kickKeyLock = false;
		playMode = 0;
		playSide = 0;
	}

	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.connectionStateDetailed.ToString () == "Joined") {
			connectCamera.SetActive (false);
		}
		if (timer > 0f) {
			TimerText ();
		}
		if (PhotonNetwork.playerList.Length < limitPlayer) {
			kickPlayer.SetActive (true);
			kickKeyLock = true;
		}
	}

	public void OnClickPauseSelect(){
		allButton.SetActive (true);
		pause = true;
	}

	public void OnClickExitPause(){
		allButton.SetActive (false);
		pause = false;
	}

	public void OnClickMode1(){
		if(clickModeState && PhotonNetwork.isMasterClient){
			PlayerSoulChooseScript.Instance.ClickMode1 ();
			mode1But.interactable = false;
			mode2But.interactable = false;
			clickModeState = false;
		}
	}
	public void OnClickMode2(){
		if (clickModeState && PhotonNetwork.isMasterClient) {
			PlayerSoulChooseScript.Instance.ClickMode2 ();
			mode1But.interactable = false;
			mode2But.interactable = false;
			clickModeState = false;
		}
	}

	public void CalculateMode(){
		/*if (mode [1] > mode [2]) {
			playMode = 1;
		} else if (mode [1] < mode [2]) {
			playMode = 2;
		} else {
			int randMode = Random.Range (1, 100);
			if (randMode % 2 == 1) {
				playMode = 1;
			} else {
				playMode = 2;
			}
		}*/
	}


	private void TimerText(){
		timer -= Time.deltaTime;
		timeToStr = (int)timer;
		clockCount.text = timeToStr.ToString ();
	}

	public void CalculateSide(){
		//int[] use = new int[PhotonNetwork.playerList.Length];
		/*int side1 = 0;
		int side2 = 0;
		int side0 = 0;
		bool toGameScene = false;
		while (!toGameScene) {
			int kni = 0;
			int mon = 0;
			for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
				if (side [i] == 1) {
					kni++;
				} 
				if (side [i] == 2) {
					mon++;
				}
			}

			for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
				if (side [i] == 0) {
					if (kni < PhotonNetwork.playerList.Length / 2) {
						side [i] = 1;
						kni++;
						continue;
					}
					if (mon < PhotonNetwork.playerList.Length / 2) {
						side [i] = 2;
						mon++;
						continue;
					}
				}
			}
			//check
			for (int i = 0; i < limitPlayer && !toGameScene; i++) {
				if (side [i] == 0) {
					break;
				}
				if (i + 1 == limitPlayer) {
					toGameScene = true;
					break;
				}
			}

		}*/
	}
}
