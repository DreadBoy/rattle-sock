using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {
    public int length = 7;
	public float speed = 1f;
	public Object člen;
	private List<Object> členi = new List<Object>();
	private float time_span = 0;
    private struct orientation
    {
        public static Quaternion up = Quaternion.Euler(0, 0, 0);
        public static Quaternion right = Quaternion.Euler(0, 90, 0);
        public static Quaternion down = Quaternion.Euler(0, 180, 0);
        public static Quaternion left = Quaternion.Euler(0, 270, 0);
    }

	// Use this for initialization
	void Start () {
        resetLevel();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.RightArrow) && transform.localRotation != orientation.left) {
			transform.localRotation = orientation.right;
		}
		if (Input.GetKey (KeyCode.DownArrow) && transform.localRotation != orientation.up) {
			transform.localRotation = orientation.down;
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.localRotation != orientation.right) {
			transform.localRotation = orientation.left;
		}
		if (Input.GetKey (KeyCode.UpArrow) && transform.localRotation != orientation.down) {
			transform.localRotation = orientation.up;
		}
		time_span += Time.deltaTime;
		if(time_span > speed*0.2f){
			time_span -= speed*0.2f;
			moveSock();
		}


	}

	private void moveSock()
	{
		transform.position += transform.forward;
		Destroy (členi[0]);
		členi.RemoveAt (0);
		GameObject predzadnji_člen = (GameObject) členi[členi.Count - 1];
		predzadnji_člen.collider.enabled = true;
		členi.Add (Instantiate(člen, transform.position - transform.forward, Quaternion.identity));
		GameObject zadnji_člen = (GameObject)členi [členi.Count - 1];
		zadnji_člen.collider.enabled = false;
	}

    public void takeDamage()
    {
        //tocke--;
        //check of Game Over
        resetLevel();
    }

    private void resetLevel()
    {
        GameObject glava = GameObject.Find("Head");
        glava.transform.position = new Vector3(0.5f, 0.5f, 0.5f);
        glava.transform.rotation = orientation.up;
        for (int i = 0; i < členi.Count; i++)
            Destroy(členi[i]);
        členi.Clear();
        for (int i = length; i > 0; i--)
            členi.Add(Instantiate(člen, transform.position - transform.forward * i, Quaternion.identity));
    }

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
            takeDamage();
	}
}
