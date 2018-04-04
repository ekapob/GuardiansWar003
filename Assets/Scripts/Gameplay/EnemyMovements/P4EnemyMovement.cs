using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class P4EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy> ();

		target = P4Waypoints.points[0];
	}
	void Update()
	{

		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.2f) 
		{
			GetNextWaypoint();
		}

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex == P4Waypoints.points.Length-2) 
		{
			WarpToMidLane ();
		}

		if (wavepointIndex >= P4Waypoints.points.Length-1) 
		{
			return;
		}

		wavepointIndex++;
		target = P4Waypoints.points [wavepointIndex];
	}

	void WarpToMidLane()
	{
		this.transform.position = Manager.instance.t2MidStart.transform.position;
	}
}
