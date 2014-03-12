using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.rotation = Quaternion.identity;
			transform.Rotate(new Vector3(0, 45, 0));
		}
		/*if (Input.GetKey (KeyCode.DownArrow))
			transform.Translate (Vector3.back *  Time.deltaTime * speed);
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (Vector3.left *  Time.deltaTime * speed);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.Translate (Vector3.right *  Time.deltaTime * speed);
			*/
		transform.Translate (transform.forward * Time.deltaTime);

	}
}
