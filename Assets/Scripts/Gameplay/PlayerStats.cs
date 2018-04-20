﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public static PlayerStats Instance;
	public static int Money;
	public int startMoney = 1000;
	[Header("Tower 1")]
	public GameObject tw1lv1;
	public GameObject tw1lv2;
	public GameObject tw1lv3;
	[Header("Tower 2")]
	public GameObject tw2lv1;
	public GameObject tw2lv2;
	public GameObject tw2lv3;
	[Header("Tower 3")]
	public GameObject tw3lv1;
	public GameObject tw3lv2;
	public GameObject tw3lv3;


	void Start()
	{
		Instance = this;
		Money = startMoney;
	}
}
