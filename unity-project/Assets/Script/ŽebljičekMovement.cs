using UnityEngine;
using System.Collections;

public class ŽebljičekMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        //vrtenje okrog osi
        transform.Rotate(new Vector3(0.0f, 0.0f, 10.0f * Time.timeScale));
       
    }

    void OnCollisionEnter(Collision collision)
    {
        //bounce
    }
}
