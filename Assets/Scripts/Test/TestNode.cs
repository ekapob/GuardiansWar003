using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : MonoBehaviour {

	public int nodeNo;
	public Color hoverColor;
	[SerializeField]
	private GameObject turret;
	[SerializeField]
	private Turret turretScript;
	private Renderer rend;
	private Color startColor;
	public static TestNode Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
		turret = null;
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}


	public void OnMouseEnter(){
		if (!CanvasGameplayControl.Instance.winStat) {
			if (turret == null) {
				if (PlayerNetwork.Instance.joinRoomNum.ToString () == tag)
					rend.material.color = hoverColor;
			}
		}
	}

	public void OnMouseDown(){
		if (!CanvasGameplayControl.Instance.winStat) {
			if (turret != null) {
				if(PlayerNetwork.Instance.joinRoomNum.ToString () == tag)
					turretScript.turretUI.SetActive (true);
			} else {
				if (Manager.instance.buildName == null) {
					return;
				}
				if (PlayerNetwork.Instance.joinRoomNum.ToString () == tag) {
					CameraController.Instance.currentClickNode = nodeNo;
					CameraController.Instance.CreateTower (Manager.instance.buildName);
					Manager.instance.buildName = null;
				}
			}
		}
	}

	public void OnMouseExit(){
		rend.material.color = startColor;
		//turretScript.turretUI.SetActive (false);
	}

	public void SetTurret(GameObject obj,Turret script){
		turret = obj;
		turretScript = script;
	}

	public void SetNodeToNull(){
		turret = null;
	}

	public Turret GetTurretOnNode(){
		return turretScript;
	}

}