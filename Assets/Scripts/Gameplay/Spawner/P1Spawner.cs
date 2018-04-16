using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class P1Spawner : MonoBehaviour {

	public static P1Spawner Instance;
	public Transform enemy1Prefab;
	public Transform enemy2Prefab;
	public Transform enemy3Prefab;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public Text waveCountdownText;

	private int waveIndex = 0;
	[Header("Unit 1 Stat")]
	public float baseHp1 = 100f;
	public float baseSpd1 = 10f;
	public float baseSize1 = 1f;
	[Header("Unit 2 Stat")]
	public float baseHp2 = 100f;
	public float baseSpd2 = 10f;
	public float baseSize2 = 1f;
	[Header("Unit 3 Stat")]
	public float baseHp3 = 100f;
	public float baseSpd3 = 10f;
	public float baseSize3 = 1f;
	[Header("Unit 4 Stat")]
	public float baseHp4 = 100f;
	public float baseSpd4 = 10f;
	public float baseSize4 = 1f;

	void Start()
	{
		if(PhotonNetwork.isMasterClient)
			Instance = this;
	}

	void Update()
	{
		
		if (countdown <= 0f) 
		{
			if(PhotonNetwork.isMasterClient)
				StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;

		waveCountdownText.text = Mathf.Round (countdown).ToString ();
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		SpawnEnemy1(baseHp1,baseSpd1,baseSize1);
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy1(baseHp2,baseSpd2,baseSize2);
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy2(baseHp3,baseSpd3,baseSize3);
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy3(baseHp4,baseSpd4,baseSize4);
	}

	void SpawnEnemy1(float a,float b,float c)
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy1Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		Enemy objScript = unit.GetComponent<Enemy> ();
		objScript.SetStat (a, b, c);
	}

	void SpawnEnemy2(float a,float b,float c)
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy2Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		Enemy objScript = unit.GetComponent<Enemy> ();
		objScript.SetStat (a, b, c);
	}

	void SpawnEnemy3(float a,float b,float c)
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy3Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		Enemy objScript = unit.GetComponent<Enemy> ();
		objScript.SetStat (a, b, c);
	}

	public void UpgradeUnit(int pos){
		if (pos == 1) {
			baseHp1 *= 2;
			baseSpd1 *= 1.25f;
			baseSize1++;
		} else if (pos == 2) {
			baseHp2 *= 2;
			baseSpd2 *= 1.25f;
			baseSize2++;
		} else if (pos == 3) {
			baseHp3 *= 2;
			baseSpd3 *= 1.25f;
			baseSize3++;
		} else if (pos == 4) {
			baseHp4 *= 2;
			baseSpd4 *= 1.25f;
			baseSize4++;
		}
	}
}
