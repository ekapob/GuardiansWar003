using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentButPackScript : MonoBehaviour {

	public GameObject sentButPack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			sentButPack.SetActive (false);
		}
	}

	public void OnClickCloseSentBut(){
		sentButPack.SetActive (false);
	}

	public void OnClickOpenSentBut(){
		sentButPack.SetActive (true);
	}

	public void OnClickSentUnit1Lv1(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,1,path);
	}
	public void OnClickSentUnit1Lv2(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,1,path);
	}
	public void OnClickSentUnit1Lv3(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,1,path);
	}

	public void OnClickSentUnit2Lv1(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,2,path);
	}
	public void OnClickSentUnit2Lv2(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,2,path);
	}
	public void OnClickSentUnit2Lv3(string path){
		CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide,2,path);
	}
}
