using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {
	public float speed = 1;
	public Object člen;
	private List<Object> členi = new List<Object>();
	private float time_span = 0;

	// Use this for initialization
	void Start () {
		for(int i = 5; i > 0; i--)
		členi.Add(Instantiate(člen, transform.position + Vector3.back * i, Quaternion.identity));
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.RightArrow) && transform.localRotation != Quaternion.Euler(0, 270, 0)) {
			transform.localRotation = Quaternion.Euler(0, 90, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow) && transform.localRotation != Quaternion.Euler(0, 0, 0)) {
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.localRotation != Quaternion.Euler(0, 90, 0)) {
			transform.localRotation = Quaternion.Euler(0, 270, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow) && transform.localRotation != Quaternion.Euler(0, 180, 0)) {
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		time_span += Time.deltaTime;
		if(time_span > 1){
			time_span -= 1;
			moveSock();
		}
		//transform.position += transform.forward * Time.deltaTime * speed;


	}

	private void moveSock()
	{
		transform.position += transform.forward;
		Destroy (členi[0]);
		členi.RemoveAt (0);
		členi.Add (Instantiate(člen, transform.position - transform.forward, Quaternion.identity));
	}
}
