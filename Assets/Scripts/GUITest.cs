using UnityEngine;
//using UnityEditor;
using System.Collections;

public class GUITest : MonoBehaviour {
	
	private Texture2D icon;
	public GameMaster gm;
	private Rect windowRect = new Rect (20, 20, 120, 50);
	Canvas canv;

	
	void Start(){
		gm = FindObjectOfType<GameMaster> ();
		canv =  FindObjectOfType<Canvas>();
	}


	void OnGUI () {

		canv.enabled = false;

		if (gm.showGUI) {
			
		
			canv.enabled = true;

			//Set the GUIStyle style to be label
			GUIStyle style = GUI.skin.GetStyle ("box");
			GUIStyle style2 = GUI.skin.GetStyle ("label");
			
			//Set the style font size to increase and decrease over time
			style.fontSize = (int)(35);
			style2.fontSize = (int)(35);

			icon = Resources.Load(gm.guiIconName+"", typeof(Texture2D)) as Texture2D;

			GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height));

			GUI.Box(new Rect (0, 0, Screen.width, Screen.height), "Score Board!");

			//GUI.Label (new Rect (Screen.width/3, 20.0f, 400, 80), "Score Board!");


			GUI.Label (new Rect (Screen.width/2.6f, 100, 200, 100), icon);

		
			GUI.Label (new Rect (30, 250, Screen.width-10, 200), "   Parabenss Voce Pescou O SUPER UPER \t\t\t  LOOPER PEIXE!!!");
			

			GUI.EndGroup ();

		}
	}

}
