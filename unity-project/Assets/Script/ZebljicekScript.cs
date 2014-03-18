using UnityEngine;
using System.Collections;

public class ZebljicekScript : MonoBehaviour {

    float hitrost = 10.0f;
    Vector3 smer = new Vector3(1.0f, 0.0f, 1.0f);

    void OnCollisionEnter(Collision collision)
    {
        //bounce
    }

    private void pozicioniraj()
    {
        transform.position = new Vector3(Random.Range(15.5f, -14.5f), 0.6f, Random.Range(15.5f, -14.5f));
        //ce je pozicija v centru (nocemo umret takoj na zacetku igre)
        if ((transform.position.x > -6.5f && transform.position.x < 6.5f) || (transform.position.z > -9.0f && transform.position.z < 9.0f))
        {
            //fakn'rekurzija, SON!
            pozicioniraj();

            //daj zebljicku hitrost
            rigidbody.velocity = smer * hitrost;
        }
    }

	// Use this for initialization
	void Start () {
	    //pozicioniraj zebljicek (vstran od centra, izven zidov)
        pozicioniraj();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
