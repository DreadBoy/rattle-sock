using Assets.Script;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int length = 7;
    public float speed = 0.25f;
    public Object tail_part;
    public int tocke = 5;
    private List<Object> tail = new List<Object> ();
    private float time_span = 0;
    public bool right = false;
    public bool left = false;
    public bool first_person = true;
    Orientation orientation = new Orientation ();

    // Use this for initialization
    void Start ()
    {
        resetLevel ();
    }
  
    // Update is called once per frame
    void Update ()
    {

        if (Input.GetKey (KeyCode.RightArrow)) { //če je pritisnil desno tipko
            if (first_person) {
                if (!right) //če prej ni bila desno
                    orientation.rotateRight ();
            } else if (orientation.Direction != Orientation.direction.LEFT) { //če prej ni bil obrnjen levo
                orientation.Direction = Orientation.direction.RIGHT;
            }
            right = true;
        } else if (Input.GetKey (KeyCode.DownArrow)) {
            if (!first_person) {
                if (orientation.Direction != Orientation.direction.UP)
                    orientation.Direction = Orientation.direction.DOWN;
            }
        } else if (Input.GetKey (KeyCode.LeftArrow)) {
            if (first_person) {
                if (!left)
                    orientation.rotateLeft ();
            } else if (orientation.Direction != Orientation.direction.RIGHT) {
                orientation.Direction = Orientation.direction.LEFT;
            }
            left = true;
        } else if (Input.GetKey (KeyCode.UpArrow)) {
            if (!first_person) {
                if (orientation.Direction != Orientation.direction.DOWN) 
                    orientation.Direction = Orientation.direction.UP;
            }
        }
        transform.localRotation = orientation.getQuaternion ();
        if (!Input.GetKey (KeyCode.RightArrow))
            right = false;
        if (!Input.GetKey (KeyCode.LeftArrow))
            left = false;
        time_span += Time.deltaTime;
        if (time_span > 0.2f / speed) {
            time_span -= 0.2f / speed;
            moveSock ();
        }

        if (tocke < 1) {
            GameObject.Find ("Zgornja vrata").renderer.enabled = false;
            GameObject.Find ("NextLevel").collider.enabled = true;
        }
        
        //Debug.Log(GameObject.FindGameObjectsWithTag("Objective").Length);
        if (GameObject.FindGameObjectsWithTag ("Objective").Length == 0) {
            GameObject.Find ("Zgornja vrata").renderer.enabled = false;
            GameObject.Find ("NextLevel").collider.enabled = true;
        }
    }

    private void moveSock ()
    {
        transform.position += transform.forward;
        Destroy (tail [0]);
        tail.RemoveAt (0);
        tail.Add (Instantiate (tail_part, transform.position - transform.forward, Quaternion.identity));
        ((GameObject)tail [tail.Count - 1]).collider.enabled = true;
    }

    public void takeDamage ()
    {
        tocke--;
        //check of Game Over
        resetLevel ();
    }

    private void resetLevel ()
    {
        transform.position = new Vector3 (0.5f, 0.5f, -14.5f);
        //obrni navzgor
        orientation.Direction = Orientation.direction.UP;
        transform.rotation = orientation.getQuaternion ();
        for (int i = 0; i < tail.Count; i++)
            Destroy (tail [i]);
        tail.Clear ();
        for (int i = length; i > 0; i--)
            tail.Add (Instantiate (tail_part, transform.position - transform.forward * i, Quaternion.identity));
    }

    void OnTriggerEnter (Collider other)
    {

        if (other.tag == "Enemy")
            takeDamage ();

        if (other.tag == "Objective") {
            tail.Add (Instantiate (tail_part, transform.position - transform.forward, Quaternion.identity));
            ((GameObject)tail [tail.Count - 1]).collider.enabled = true;
            Debug.Log ("objective touched");
            Destroy (other.gameObject);
            tocke++;
        }
    }
}
