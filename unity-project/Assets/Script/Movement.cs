using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public int length = 7;
	public float speed = 2f;
	public Object tail_part;
    public int tocke = 5;
	private List<Object> tail = new List<Object>();
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
		else if (Input.GetKey (KeyCode.DownArrow) && transform.localRotation != orientation.up) {
			transform.localRotation = orientation.down;
		}
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.localRotation != orientation.right)
        {
			transform.localRotation = orientation.left;
		}
        else if (Input.GetKey(KeyCode.UpArrow) && transform.localRotation != orientation.down)
        {
			transform.localRotation = orientation.up;
		}
		time_span += Time.deltaTime;
        if (time_span > 0.2f / speed)
        {
            time_span -= 0.2f / speed;
			moveSock();
		}
        if (tocke < 1)
        {
            GameObject.Find("Zgornja vrata").renderer.enabled = false;
            GameObject.Find("NextLevel").collider.enabled = true;
        }


	}

	private void moveSock()
	{
		transform.position += transform.forward;
		Destroy (tail[0]);
		tail.RemoveAt (0);
        tail.Add(Instantiate(tail_part, transform.position - transform.forward, Quaternion.identity));
        ((GameObject)tail[tail.Count - 1]).collider.enabled = true;
	}

    public void takeDamage()
    {
        tocke--;
        //check of Game Over
        resetLevel();
    }

    private void resetLevel()
    {
        transform.position = new Vector3(0.5f, 0.5f, -14.5f);
        transform.rotation = orientation.up;
        for (int i = 0; i < tail.Count; i++)
            Destroy(tail[i]);
        tail.Clear();
        for (int i = length; i > 0; i--)
            tail.Add(Instantiate(tail_part, transform.position - transform.forward * i, Quaternion.identity));
    }

	void OnTriggerEnter(Collider other) {

        if (other.tag == "Enemy")
            takeDamage();

        if (other.tag == "Objective")
        {
            Debug.Log("objective touched");
            Destroy(other.gameObject);
            tocke++;
        }
	}
}
