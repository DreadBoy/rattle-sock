using UnityEngine;
using System.Collections;

public class ZebljicekScript : MonoBehaviour {

    float hitrost = 0.1f;
    Vector3 smer = new Vector3(1.0f, 0.0f, 1.0f);

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        //smer.x = smer.x * (collision.contacts[0].normal.x * (-1));
        //smer.z = smer.z * (collision.contacts[0].normal.z * (-1));
    }

    private void pozicioniraj()
    {
        transform.position = new Vector3(Random.Range(15.5f, -14.5f), 0.6f, Random.Range(15.5f, -14.5f));
        //ce je pozicija v centru (nocemo umret takoj na zacetku igre)
        if ((transform.position.x > -6.5f && transform.position.x < 6.5f) || (transform.position.z > -9.0f && transform.position.z < 9.0f))
        {
            //fakn'rekurzija, SON!
            pozicioniraj();
        }
    }

	// Use this for initialization
	void Start () {
	    //pozicioniraj zebljicek (vstran od centra, izven zidov)
        pozicioniraj();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(smer * hitrost);
	}
}
