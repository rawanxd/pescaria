using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed;
	private bool pescado = false;
	public GameMaster gm;
	private Sprite fishSprite;
	private SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		moveSpeed = 4;
		gm = FindObjectOfType<GameMaster> ();
		spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pescado && !gm.showGUI) {

			if (transform.position.x < -8) {;
				RotateRight();
			} else if (transform.position.x > 8) {
				RotateLeft();
			}

			transform.Translate ((moveSpeed * -1) * Time.deltaTime, 0, 0);
		}
		else {
			if(transform.position.y > 4){
				Destroy(transform.gameObject);
				gm.guiIconName = gameObject.tag;
				gm.showGUI = true;
				gm.trumble = false;
			}
			else if(transform.position.y > 1.7f){
				transform.localScale = new Vector3(2,1,1.0f);
				fishSprite = Resources.Load(""+gameObject.tag+"", typeof(Sprite)) as Sprite;
				spr.sprite = fishSprite;

				if(gameObject.tag == "Enemy4")
					gm.trumble = true;

			}
		}

	}

	void OnTriggerEnter2D(Collider2D coliderElement) {
		Debug.Log ("Colision");
		if (!gm.showGUI) {
			// Disable collisions with the object being attached
			GameObject coliderObj = coliderElement.gameObject;
			GameObject myObj = transform.gameObject;
		
			if (coliderObj.transform.childCount < 1) {

				myObj.transform.parent = coliderObj.transform;

				// make sure its exactly on it
				myObj.transform.localPosition = Vector3.zero;
				myObj.transform.localRotation = Quaternion.identity;
				pescado = true;
			
				// Disable collisions with the object being attached
				if (myObj.GetComponent<Collider2D> ())
					myObj.GetComponent<Collider2D> ().enabled = false;
			
				// Don't allow physics to affect the object
				if (myObj.GetComponent<Rigidbody2D> ())
					myObj.GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}
	}

	void RotateRight () {
		transform.rotation = Quaternion.Euler(0, 180, 0);
	}

	void RotateLeft () {
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
	
}
