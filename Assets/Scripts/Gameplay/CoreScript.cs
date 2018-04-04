using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreScript : MonoBehaviour {

	public float coreHealth;
	public float coreCurrentHealth;

	public GameObject winPopUp;

	public Image coreHealthBar;

	void Start () 
	{
		coreCurrentHealth = coreHealth;
	}

	public void CoreTakeDamage(float amount)
	{
		coreCurrentHealth -= amount;
		coreHealthBar.fillAmount = coreCurrentHealth/coreHealth;
		if (coreCurrentHealth <= 0)
			Die ();
	}

	void Die()
	{
		winPopUp.SetActive (true);
		//----------------------------------------------------------
		Destroy (gameObject);
	}
}
