using UnityEngine;
using System.Collections;

public class backNforth : MonoBehaviour {
private int test = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(Vector3.forward * Time.deltaTime * test * 0.3f);
		if ( transform.position.z >= 5 || transform.position.z <= -5 )
			test = -1 * test;
	}
}
