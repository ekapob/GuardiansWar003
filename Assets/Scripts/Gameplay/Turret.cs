﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Turret : Photon.MonoBehaviour {

	private Transform target;
	private PhotonView PhotonView;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	public int onNode;
	public GameObject turretUI;
	public GameObject upGradePrefab;

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets(default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = 0.5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	// Use this for initialization
	void Start () {
		turretUI.SetActive (false);
		onNode = CameraController.Instance.currentClickNode;
		/*if (!photonView.isMine)
			gameObject.SetActive (false);*/
		PhotonView = GetComponent<PhotonView> ();
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

	// Update is called once per frame
	void Update () {
		if (target == null) 
		{
			if (useLaser) 
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser) {
			Laser ();
		} else 
		{
			if (fireCountdown <= 0f) {
				Shoot ();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	void Laser()
	{
		target.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
		target.GetComponent<Enemy> ().Slow(slowAmount);

		if (!lineRenderer.enabled) 
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition (0, firePoint.position);
		lineRenderer.SetPosition (1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		GameObject bulletGO = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit", bulletPrefab.name), firePoint.position, firePoint.rotation, 0);
		//GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		partToRotate.rotation = Quaternion.RotateTowards (partToRotate.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (partToRotate.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}

	public void OnClickSell(){
		if (photonView.isMine) {
			if (PlayerNetwork.Instance.joinRoomNum == 1) {
				TestNode node = TestNode1.Instance.node [onNode];
				node.SetNodeToNull ();
			} else if (PlayerNetwork.Instance.joinRoomNum == 2) {
				TestNode node = TestNode2.Instance.node [onNode];
				node.SetNodeToNull ();
			}
			PhotonNetwork.Destroy (gameObject);
		}
	}

	public void ShowUI(){
		turretUI.SetActive (true);
	}

	public void HideUI(){
		turretUI.SetActive (false);
	}

	public void OnClickUpgrade(){
		GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", upGradePrefab.name), transform.position, transform.rotation, 0);	
		Turret objScript = objTurret.GetComponent<Turret> ();
		if (PlayerNetwork.Instance.joinRoomNum == 1) {	
			TestNode1.Instance.node [onNode].SetTurret (objTurret, objScript);
		}
		else if (PlayerNetwork.Instance.joinRoomNum == 2) {	
			TestNode2.Instance.node [onNode].SetTurret (objTurret, objScript);
		}
		PhotonNetwork.Destroy (gameObject);
	}

}