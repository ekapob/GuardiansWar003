using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class P1EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy> ();

		target = P1Waypoints.points[0];
	}
	void Update()
	{
		if (PhotonNetwork.isMasterClient) {
			Vector3 dir = target.position - transform.position;
			transform.Translate (dir.normalized * enemy.speed * Time.deltaTime, Space.World);

			if (Vector3.Distance (transform.position, target.position) <= 0.2f) {
				GetNextWaypoint ();
			}

			enemy.speed = enemy.startSpeed;
		} else {
			SmoothMove ();
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex == P1Waypoints.points.Length-2) 
		{
			WarpToMidLane ();
		}

		if (wavepointIndex >= P1Waypoints.points.Length-1) 
		{
			return;
		}

		wavepointIndex++;
		target = P1Waypoints.points [wavepointIndex];
	}

	void WarpToMidLane()
	{
		this.transform.position = Manager.instance.t1MidStart.transform.position;
	}
	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}
}
