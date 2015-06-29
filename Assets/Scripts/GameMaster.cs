/*using UnityEngine;
using System;
using System.Collections.Generic;
*/
using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
	private List<string> enemyTags = new List<string>();
	private int[] maxDow = {-5,-2,-3,-4};
	private int[] maxUp = {-2,0,-1,0};
	private int[] maxEnemyNumber = {3,2,2,3};
	private int[] maxEnemySpawns = {3,2,2,3};
	private int[] qntSpaws = {0,0,0,0};
	public bool showGUI = false;
	public string guiIconName = "";
	public bool trumble = false;
	public Spawn mySpaw;
	public GUITest board;




	// Use this for initialization
	void Start () {
		enemyTags.Add ("Enemy1");
		enemyTags.Add ("Enemy2");
		enemyTags.Add ("Enemy3");
		enemyTags.Add ("Enemy4");

		mySpaw = FindObjectOfType<Spawn> ();
		board = FindObjectOfType<GUITest>();


	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i < maxUp.Length; i++){
		
			GameObject[] enemys = GameObject.FindGameObjectsWithTag (enemyTags[i]);
		
		
			if (enemys.Length < maxEnemyNumber[i] && qntSpaws[i] < maxEnemySpawns[i]) {
				var yAxis = Random.Range (maxDow[i], maxUp[i]);
				var xAxis = Random.Range (-13, 13);
				Vector3 myVet = new Vector3 (xAxis, yAxis, 0);
			
				mySpaw.SpwanEnemy (enemys [0], myVet);
				qntSpaws[i]++;
				//var temp_spwan = Instantiate (enemys[0],myVet, Quaternion.identity);
			
			}
		}
	
	}

	void updateScore(){
	/*	scoreText = FindObjectOfType<GUIText>();
		scoreText.text = "10";
		scoreText.enabled = true;
		//scoreText.transform.;*/
	}
}
