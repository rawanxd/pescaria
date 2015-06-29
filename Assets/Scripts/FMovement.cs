using UnityEngine;
using System.Collections;

public class FMovement : MonoBehaviour {
	
	Rigidbody2D rb;
	Animator anim;
	public UniMoveController moves;
	public GameMaster gm;
	private bool stopController = false;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameMaster> ();
		rb =   gameObject.GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		Time.maximumDeltaTime = 0.1f;
		
		int count = UniMoveController.GetNumConnected();
		
		// Iterate through all connections (USB and Bluetooth)
		for (int i = 0; i < count; i++)
		{
			UniMoveController move = gameObject.AddComponent<UniMoveController>();	// It's a MonoBehaviour, so we can't just call a constructor
			
			
			// Remember to initialize!
			if (!move.Init(i))
			{
				Destroy(move);	// If it failed to initialize, destroy and continue on
				continue;
			}

			// This example program only uses Bluetooth-connected controllers
			PSMoveConnectionType conn = move.ConnectionType;
			if (conn == PSMoveConnectionType.Unknown || conn == PSMoveConnectionType.USB)
			{
				Destroy(move);
			}
			else
			{	

				//move.OnControllerDisconnected += HandleControllerDisconnected;
				move.EnableOrientation ();
				move.InitOrientation();
			//	move.ResetOrientation();
				
				// Start all controllers with a white LED
				move.SetLED(Color.white);
				move.UpdateRate = 0.02f;

				moves = move;

				
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {



		UniMoveController move  = moves;
		// Instead of this somewhat kludge-y check, we'd probably want to remove/destroy
		// the now-defunct controller in the disconnected event handler below.
		//if (move.Disconnected) continue;

		// Set the rumble based on Enemy 4 has been caught
		if (gm.trumble)
			move.SetRumble (2);
		else
			move.SetRumble (0);

		// Button events. Works like Unity's Input.GetButton
		if (move.GetButtonDown(PSMoveButton.Circle)){
			Debug.Log("Circle Down");
		}
		if (move.GetButtonUp(PSMoveButton.Circle)){
			Debug.Log("Circle UP");
		}

		// Change the colors of the LEDs based on which button has just been pressed:
		if (move.GetButtonDown(PSMoveButton.Circle)){
			transform.position = (new Vector3(-7,3,0));
			rb.Sleep();
			stopController = true;
			move.SetLED(Color.red);
		}
		else if(move.GetButtonDown(PSMoveButton.Triangle)){
			stopController = false;
			move.SetLED(Color.cyan);
		}/*
		else if(move.GetButtonDown(PSMoveButton.Square)){
			move.SetLED(Color.yellow);
		}
		*/

		
		if(move.GetButtonDown(PSMoveButton.Cross)){
			gm.showGUI = false;
		}

		if(move.GetButtonDown(PSMoveButton.Move)){
			move.ResetOrientation();
			move.InitOrientation();
			move.SetLED(Color.black);
		}

		float qntMX = Mathf.Abs(move.Gyro.z) -  0.125f;

		if (qntMX > 0.10f)
			qntMX = Mathf.Pow (qntMX, 0.5f);
		else
			qntMX =  0.0f;
		/*else
			qntMX = move.Acceleration.x;

*/
		if (move.Gyro.z > 0) {
			qntMX *= -1;
		}

		Vector2 mov = new Vector2(qntMX*4, (move.Acceleration.y-0.3f)*5);

			//print (mov);

		if(!stopController)	
			rb.MovePosition (rb.position + mov * Time.maximumDeltaTime);


		if (rb.position.x > 14)
			rb.MovePosition (new Vector2 (13.7f, rb.position.y));

		if(rb.position.x < -14)
			rb.MovePosition (new Vector2 (-13.7f, rb.position.y));

		if(rb.position.y > 8)
			rb.MovePosition (new Vector2 (rb.position.x, 7.7f ));

		if(rb.position.y < -5)
			rb.MovePosition (new Vector2 (rb.position.x, -4.7f));

	//	Debug.Log ("RGB2D : "+rb.position);
//		Debug.Log("Transform : "+ transform.position);


		

			/*display += string.Format("PS Move {0}: ax:{1:0.000}, ay:{2:0.000}, az:{3:0.000} gx:{4:0.000}, gy:{5:0.000}, gz:{6:0.000}\n",
			                         i+1, moves[i].Acceleration.x, moves[i].Acceleration.y, moves[i].Acceleration.z,
			                         moves[i].Gyro.x, moves[i].Gyro.y, moves[i].Gyro.z);

			//Vector2 mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			Vector2 mov = new Vector2(moves[i].Acceleration.x, moves[i].Acceleration.y);

			if(mov != Vector2.zero){
				anim.SetBool("isWalking", true);
				anim.SetFloat("input_x",mov.x);
				anim.SetFloat("input_y",mov.y );
			}
			else{
				anim.SetBool("isWalking", false);
			}
			rb.MovePosition(rb.position + mov * Time.deltaTime*5);
		*/
	}
}