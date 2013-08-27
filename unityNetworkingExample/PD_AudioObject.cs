using UnityEngine;
using System.Collections;

public class PD_AudioObject : MonoBehaviour {
	
	Transform thisTrans;
	
	public bool isStatic;	//Active?
	public bool isPlayer;	//Player?
	public int 	sndIndex;	//Object Label
	public int play = 0;	//1 or 0, checks if playing at frame
	public int loop = 0;	//1 or 0, checks if looping while playing
	public float vol = 0.0f;		//b/w 1 or 0, volume
	public int ambience = 0;//1 or 0, ambience check
	public string filename; //name of file including extension (.wav, .aiff, etc.)
	
	private string object_position;
	// Use this for initialization
	void Start () {

		thisTrans = transform;	//object for obtaining positional data
		
		if( !isPlayer ) {
		
			sndIndex = PD_AudioManager.Instance.RequestIndex();	//assigns unique sndIndex for each new object
			
			// Send initialisation parameters
			string initmsg = "init" + sndIndex + " file " + filename + " ambient " + ambience + ";";
			
			if (initmsg != null) {
			
				PD_AudioManager.Instance.Send( initmsg );
			
			}
			
			object_position = "snd" + sndIndex  + " play " + play + " xpos " + 
				thisTrans.position.x + " zpos " + thisTrans.position.z + " loop " + loop + 
				" vol " + vol + ";";
			
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
		
			object_position = "snd" + sndIndex  + " play " + play + " xpos " + 
				thisTrans.position.x + " zpos " + thisTrans.position.z + " loop " + loop + 
				" vol " + vol + ";";
			
			if (object_position != null) {
			
				PD_AudioManager.Instance.Send( object_position );
			
			}
			
		}
		
		else if( !isPlayer && (play != old_play) ) {
		
			staticSend();
		
		}
		
		else {
				
			string player_pos = "ppx " + thisTrans.position.x + ";" + 
				" ppz " + thisTrans.position.z + ";" + 
				" prot " + thisTrans.rotation.eulerAngles.y + ";";
			
			if (player_pos != null) {
			
				PD_AudioManager.Instance.Send( player_pos );
			
			}
				
		}
		
		old_play = play;
		
	}
	
	void staticSend () {
		
		object_position = "snd" + sndIndex  + " play " + play + " xpos " + 
				thisTrans.position.x + " zpos " + thisTrans.position.z + " loop " + loop + 
				" vol " + vol + ";";
		
		if (object_position != null) {
			
				PD_AudioManager.Instance.Send( object_position );
			
		}
	
	}
		
}
