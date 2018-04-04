using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogCanvasScript : MonoBehaviour {
	public GameObject battleLog;
	// Use this for initialization
	void Start () {
		battleLog.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickShowBattleLog(){
		battleLog.SetActive (true);
	}
}
