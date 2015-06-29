using UnityEngine;
using System;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {
	
	public void SpwanEnemy(GameObject myEnemy , Vector3 respawPos){

		Instantiate (myEnemy,respawPos, Quaternion.identity);	
	}


	// Use this for initialization
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
