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

		//for (int i = 0; i < waveIndex; i++) 
		//{
			SpawnEnemy1();
			yield return new WaitForSeconds (0.5f);
			SpawnEnemy1();
			yield return new WaitForSeconds (0.5f);
			SpawnEnemy2();
			yield return new WaitForSeconds (0.5f);
			SpawnEnemy3();
			yield return new WaitForSeconds (0.5f);
		//}
	}

	void SpawnEnemy1()
	{
		PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy1Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		//Instantiate (enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
	}

	void SpawnEnemy2()
	{
		PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy2Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		//Instantiate (enemy2Prefab, spawnPoint.position, spawnPoint.rotation);
	}

	void SpawnEnemy3()
	{
		PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit/P1Enemies", enemy3Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
		//Instantiate (enemy3Prefab, spawnPoint.position, spawnPoint.rotation);
	}
}
