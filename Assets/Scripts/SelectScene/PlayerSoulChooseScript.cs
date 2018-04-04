using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoulChooseScript : Photon.MonoBehaviour {

	public static PlayerSoulChooseScript Instance;
	private int mode = 0;
	private int sidepick = 0;
	[SerializeField]
	private GameObject burn;
	private bool moveAble;
	private bool toGame = false;
	public bool UseTransformView = true;
	private PhotonView PhotonView;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	public bool m_bombStat = false;
	private int side = 0;

	// Use this for initialization
	private void Awake () {
		Instance = this;
		burn.SetActive (m_bombStat);
		moveAble = true;
		PhotonView = GetComponent<PhotonView> ();
	}

	// Update is called once per frame
	void Update () {
		if (!CanvasGameButton.Instance.kickKeyLock) {
			if (PhotonView.isMine && !CanvasGameButton.Instance.pause) {
				CheckInput ();
			} else {
				SmoothMove ();
			}
			if (CanvasGameButton.Instance.timer < 0f && moveAble) {
				moveAble = false;
				if (!toGame) {
					photonView.RPC ("RPC_ModeSend", PhotonTargets.All); 
					Invoke ("MoveToSpawner", 2.5f);
					toGame = true;
				} else if (toGame) {
					photonView.RPC ("RPC_SideSend", PhotonTargets.All); 
					//if (PhotonNetwork.isMasterClient) {
						CanvasGameButton.Instance.CalculateMode ();// CALCULATE MODE
						CanvasGameButton.Instance.CalculateSide ();// CALCULATE SIDE
						photonView.RPC ("RPC_ModePlay", PhotonTargets.All); 
						photonView.RPC ("RPC_SidePlay", PhotonTargets.All); 
					//}
					side = PlayerNetwork.Instance.joinRoomNum;
					CanvasGameButton.Instance.clockCount.text = mode.ToString();
					//MotherScript.Instance.currentGameMode = mode;
					MotherScript.Instance.currentGameMode = 1;
					MotherScript.Instance.currentGameSide = side;
					PlayerNetwork.Instance.PlayersInGame = 0;
					PhotonNetwork.LoadLevel (4);
				}
			}
		}
	}
	private void CheckInput(){
		float moveSpeed = 1f; 
		float rotateSpeed = 100f;
		burn.transform.position = transform.position;

		if (moveAble) {
			float vertical = Input.GetAxis ("Vertical");
			float horizontal = Input.GetAxis ("Horizontal");
			transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
			transform.Rotate (new Vector3 (0, horizontal * rotateSpeed * Time.deltaTime, 0));
			if (Input.GetKeyDown (KeyCode.Space)) {
				m_bombStat = true;
			}
		}
		if (m_bombStat) 
		{
			photonView.RPC ("RPC_Bomb", PhotonTargets.All); 
		}
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Mode1") {
			mode = 1;
		} else if (other.tag == "Mode2") {
			mode = 2;
		} else if (other.tag == "ModeRand") {
			mode = 0;
		}
		if (other.tag == "Side1") {
			sidepick = 1;
		} else if (other.tag == "Side2") {
			sidepick = 2;
		} else if (other.tag == "SideRand") {
			sidepick = 0;
		}
	}

	void MoveToSpawner(){
		transform.position = CanvasGameButton.Instance.spawner2 [PlayerNetwork.Instance.joinRoomNum-1].transform.position;
		transform.Rotate (new Vector3(0,0,0));
		moveAble = true;
		CanvasGameButton.Instance.timer = 10f;
		CanvasGameButton.Instance.mode1But.gameObject.SetActive (false);
		CanvasGameButton.Instance.mode2But.gameObject.SetActive (false);
		CanvasGameButton.Instance.kniBut.gameObject.SetActive (true);
		CanvasGameButton.Instance.monBut.gameObject.SetActive (true);
	}

	//Transfer data
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}

	public void ClickMode1(){
		photonView.RPC ("RPC_OnClickMode1", PhotonTargets.All); 
	}

	public void ClickMode2(){
		photonView.RPC ("RPC_OnClickMode2", PhotonTargets.All); 
	}

	[PunRPC] 
	private void RPC_Bomb()
	{ 
		burn.SetActive (true); 
	}

	//Send Number of enter zone
	[PunRPC]
	private void RPC_ModeSend(){
		CanvasGameButton.Instance.mode [mode]++;
	}
	[PunRPC]
	private void RPC_SideSend(){
		CanvasGameButton.Instance.side [PlayerNetwork.Instance.joinRoomNum - 1] = sidepick;
	}

	[PunRPC]
	private void RPC_ModePlay(){
		mode = CanvasGameButton.Instance.playMode;
	}

	[PunRPC]
	private void RPC_SidePlay(){
		side = CanvasGameButton.Instance.side [PlayerNetwork.Instance.joinRoomNum - 1];
	}

	[PunRPC]
	private void RPC_OnClickMode1(){
		CanvasGameButton.Instance.modePrintTxt.text = "Mode : KilL'a BosSSS";
		mode = 1;
		CanvasGameButton.Instance.timer = 3f;
	}

	[PunRPC]
	private void RPC_OnClickMode2(){
		CanvasGameButton.Instance.modePrintTxt.text = "Mode : Ah Base is on FIRE";
		mode = 2;
		CanvasGameButton.Instance.timer = 3f;
	}


}
