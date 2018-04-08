using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Photon.MonoBehaviour {

	Manager manager;

	//-----

	private Transform target;
	private PhotonView PhotonView;

	private float range = 2.5f;

	public GameObject enemyAttackPrefab;

	public float fireRate = 1f;
	private float fireCountdown = 0f;

	public string enemyTag = "Core";

	public Transform firePoint;

	//-----

	public float startSpeed = 10f;
	public float speed;

	public float startHealth = 100;
	public float currentHealth;

	public int worth = 50;

	public Image healthBar;
	public Animator enemyAnim;

	void Start()
	{
		manager = Manager.instance;
		currentHealth = startHealth;
		PhotonView = GetComponent<PhotonView> ();

		speed = startSpeed;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		} else
		{
			target = null;
		}
	}

	void Update () {
		if (target == null) 
		{
			return;
		}

		LockOnTarget();

		if (fireCountdown <= 0f) {
			Shoot ();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(enemyAttackPrefab, firePoint.position, firePoint.rotation);
		EnemyAttack bullet = bulletGO.GetComponent<EnemyAttack>();

		if (bullet != null)
			bullet.Seek(target);
	}

	public void TakeDamage(float amount)
	{
		float healthCheck = currentHealth - amount;
		if (healthCheck <= 0)
			PlayerStats.Money += worth;
		photonView.RPC ("RPC_Health", PhotonTargets.All,amount); 
	}

	public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	[PunRPC]
	private void RPC_Die()
	{
		enemyAnim.Play ("Death", -1, 0f);
		Invoke ("Die",1.25f);
	}

	[PunRPC]
	private void RPC_Health(float dam){
		currentHealth -= dam;
		healthBar.fillAmount = currentHealth/startHealth;
		if (currentHealth <= 0) {
			photonView.RPC ("RPC_Die", PhotonTargets.All); 
		}
	}

	void Die(){
		PhotonView.Destroy(gameObject);
	}
}
