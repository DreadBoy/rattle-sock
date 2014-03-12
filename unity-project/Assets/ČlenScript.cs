using UnityEngine;
using System.Collections;

public class ČlenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
			print ("Zaletel si se!" + other.name);
	}
}
