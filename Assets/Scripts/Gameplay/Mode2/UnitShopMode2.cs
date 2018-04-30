using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitShopMode2 : MonoBehaviour {

	public static UnitShopMode2 Instance;
	public Texture2D[] knightPic;
	public Texture2D[] monsterPic;
	public RawImage[] shopImg;
	// Use this for initialization
	void Start () {
		Instance = this;
		if (MotherScript.Instance.currentGameSide == 1 || MotherScript.Instance.currentGameSide == 3) {
			shopImg [0].texture = knightPic [0];
			shopImg [1].texture = knightPic [1];
		} else {
			shopImg [0].texture = monsterPic [0];
			shopImg [1].texture = monsterPic [1];
		}
		
	}

}
