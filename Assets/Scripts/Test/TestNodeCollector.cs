using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNodeCollector : MonoBehaviour {

	public static TestNodeCollector Instance;
	public TestNode[] node;
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
