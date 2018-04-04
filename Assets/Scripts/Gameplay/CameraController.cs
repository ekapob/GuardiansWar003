﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraController : Photon.MonoBehaviour {

	public static CameraController Instance;
	private bool doMovement = true;
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;
	private PhotonView PhotonView;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	private GameObject turretToBuildx; // prefab of tower need to add more
	public GameObject standardTurretPrefabx;
	public int currentClickNode;

	void Start (){
		Debug.Log ("Master : " + PhotonNetwork.isMasterClient);
		Instance = this;
		PhotonView = GetComponent<PhotonView> ();
		CanvasGameplayControl.Instance.loadingImg.SetActive (false);
		photonView.RPC ("RPC_SendSideGameplay",PhotonTargets.All);
		turretToBuildx = standardTurretPrefabx;
	}
	void Update ()
	{
		if (!PauseAndExitButton.Instance.pause && photonView.isMine) {
			MoveCode ();
		}
	}

	private void MoveCode(){
		if (Input.GetKeyDown (KeyCode.Escape))
			doMovement = !doMovement;

		if (!doMovement)
			return;

		if (Input.GetKey ("w") /*|| Input.GetKey (KeyCode.UpArrow)/*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
			transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("s") /*|| Input.GetKey (KeyCode.DownArrow)|| Input.mousePosition.y <= panBorderThickness*/) {
			transform.Translate (Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("d") /*|| Input.GetKey (KeyCode.RightArrow)|| Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
			transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("a") /*|| Input.GetKey (KeyCode.LeftArrow)|| Input.mousePosition.x <= panBorderThickness*/) {
			transform.Translate (Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis ("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y += scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp (pos.y, minY, maxY);

		transform.position = pos;
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
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


	[PunRPC]
	private void RPC_SendSideGameplay(){
		//Bug
		//CanvasGameplayControl.Instance.sidePlayer[MotherScript.Instance.currentGameSide]++;
	}

	public void CreateTower(string name){
		if (PlayerNetwork.Instance.joinRoomNum == 1) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode1.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			TestNode1.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
		}
		else if (PlayerNetwork.Instance.joinRoomNum == 2) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode2.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			TestNode2.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
		}
	}


}