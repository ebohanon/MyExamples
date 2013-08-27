using UnityEngine;
using System.Collections;

public class audioObjectTest : MonoBehaviour {
	
	//Transform thisTrans;
	
	public bool isStatic = false;	//Active?
	public bool isPlayer = false;	//Player?
	public int 	sndIndex;	//Object Label
	public int play = 0;	//1 or 0, checks if playing at frame
	public int loop = 0;	//1 or 0, checks if looping while playing
	public float vol = 0.0f;//b/w 100 or 0, volume
	public int ambience = 0;//1 or 0, ambience check
	public string filename = "Room1.wav"; //name of file including extension (.wav, .aiff, etc.)
	public float testx = 0.5f;
	public float testz = 0.5f;
	
	private string object_position;
	// Use this for initialization
	void Start () {

		//thisTrans = transform;	//object for obtaining positional data
		
		if( !isPlayer ) {
		
			sndIndex = PD_AudioManager.Instance.RequestIndex();	//assigns unique sndIndex for each new object
			
			// Send initialisation parameters
			string initmsg = "init" + sndIndex + " file " + filename + " ambient " + ambience + ";";
		
			if (initmsg != null) {
			
				PD_AudioManager.Instance.Send( initmsg );
			
			}
			
			object_position = "snd" + sndIndex + " play " + play + " xpos " + 
				testx + " zpos " + testz + 
				" loop " + loop + " vol " + 70.0f + ";";
			
			if (object_position != null) {
			
				PD_AudioManager.Instance.Send( object_position );
			
			}
		
		}
		else {
			
			sndIndex = -1;	//Player will always have -1 for index
		
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int old_play = 0; //used for static objects
		
		//Checks if sending player data or static/moving sound data
		if( !isStatic && !isPlayer ) {
		
			object_position = "snd" + sndIndex + " play " + play + " xpos " + 
				testx + " zpos " + testz + 
				" loop " + loop + " vol " + 70.0f + ";";
			
			if (object_position != null) {
			
				PD_AudioManager.Instance.Send( object_position );
			
			}
			
		}
		
		else if( !isPlayer && (play != old_play) ) {
		
			staticSend();
		
		}
		
		else {
				
			string player_pos = "ppx " + 0 + ";" + 
				" ppz " + 0 + ";" + 
				" prot " + 0 + ";";
			
			if (player_pos != null) {
			
				PD_AudioManager.Instance.Send( player_pos );
			
			}
				
		}
		
		old_play = play;
		if ( testx >= 2.1f )
			testx = -2.1f;
		else
			testx += 0.01f;
		if ( testz >= 2.1f )
			testz = -2.1f;
		else
			testz += 0.02f;
		
	}
	
	void staticSend () {
		
		object_position = "snd" + sndIndex + " xpos " + 
			Random.Range(0.0f, 3.5f) + " zpos " + Random.Range(0.0f, 3.5f) + 
			" play " + Random.Range(0,1) + " loop " + Random.Range(0,1) + " vol " + Random.Range(0.0f,100.0f) + ";";
		
		if (object_position != null) {
			
				PD_AudioManager.Instance.Send( object_position );
			
		}
	
	}
		
}
