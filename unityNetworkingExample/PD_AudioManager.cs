using UnityEngine;

using System.Collections;

using System.IO;

using System.Text;

using System.Net.Sockets;

public class PD_AudioManager : MonoBehaviour {
	
	//Singleton class declaration
	private static PD_AudioManager instance = null;
	
	public static PD_AudioManager Instance {
		
		get {
			
			if( instance == null ) {
				instance = FindObjectOfType( typeof(PD_AudioManager) ) as PD_AudioManager;
				//instance.Init();
			}
		
			if( instance == null ) {
			
				GameObject pd_audiomanager_go = new GameObject("PD_AudioManager");
				
				instance = pd_audiomanager_go.AddComponent< PD_AudioManager >();
				//instance.Init();				
			}
			return instance;
		}
		
		
	}
	
	public bool allowDebug = true;
	
	//Networking variable declaration
	private TcpClient client;

    private Stream stream;

    private int curIndex = 0;
	
	public int NUMBER_OF_SOUNDS = 1;	//assign value before
	
	// Use this for initialization
	void Awake () {
		
		DebugMessage( "PD_AudioManager Start");
		
		//networking variable creation and construction
		client = new TcpClient();

        client.Connect("127.0.0.1", 32000);

        stream = client.GetStream();

		string pdCreation = "create " + NUMBER_OF_SOUNDS + ";";
		
		Send(pdCreation);
		
	}
	
	void OnApplicationQuit() {
	
		
		client.Close();
		
	}
	
	public void Send(string msg) {
	
		
			
		//parsing of string into network data
		System.Text.ASCIIEncoding  encoding=new System.Text.ASCIIEncoding();
    	
		byte[] ba = encoding.GetBytes( msg );
		
		if( stream != null )
        	stream.Write( ba, 0, ba.Length );
	
		DebugMessage( msg );
	}
	
	public int RequestIndex() {
		
		//assigns unique indices to each object requesting an index
		curIndex++;
		
		return curIndex;
		
	}
	
	void DebugMessage( string msg ) {
		
		if( allowDebug ) 
			Debug.Log( msg );
	}
}
