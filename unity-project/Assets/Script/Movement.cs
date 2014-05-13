using Assets.Script;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Object tail_part;
    private List<Object> tail = new List<Object>();
    private float time_span = 0;

    public bool first_person = true;
    Orientation orientation = new Orientation();
    struct keyboard_state
    {
        public static bool right;
        public static bool left;
    }


    // Use this for initialization
    void Start()
    {
        resetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        { //če je pritisnil desno tipko
            if (first_person)
            {
                if (!keyboard_state.right) //če prej ni bila pristisnjena desno
                    orientation.rotateRight();
            }
            else if (orientation.Direction != Orientation.direction.LEFT)
            { //če prej ni bil obrnjen levo
                orientation.Direction = Orientation.direction.RIGHT;
            }
            keyboard_state.right = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!first_person)
            {
                if (orientation.Direction != Orientation.direction.UP)
                    orientation.Direction = Orientation.direction.DOWN;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (first_person)
            {
                if (!keyboard_state.left)
                    orientation.rotateLeft();
            }
            else if (orientation.Direction != Orientation.direction.RIGHT)
            {
                orientation.Direction = Orientation.direction.LEFT;
            }
            keyboard_state.left = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!first_person)
            {
                if (orientation.Direction != Orientation.direction.DOWN)
                    orientation.Direction = Orientation.direction.UP;
            }
        }
        //nastavi rotacijo glede na spremembe zgoraj
        transform.localRotation = orientation.getQuaternion();
        //preveri spuščene tipke
        if (!Input.GetKey(KeyCode.RightArrow))
            keyboard_state.right = false;
        if (!Input.GetKey(KeyCode.LeftArrow))
            keyboard_state.left = false;
        //prištej števec
        time_span += Time.deltaTime;
        //če je čas za nov update
        if (time_span > 0.2f / GameManager.move_speed)
        {
            time_span -= 0.2f / GameManager.move_speed;
            moveSock();
        }


        //preveri, če smo pojedli vsa jabolka
        if (GameObject.FindGameObjectsWithTag("Objective").Length == 0)
        {
            GameObject.Find("Zgornja vrata").renderer.enabled = false;
            GameObject.Find("Zgornja vrata").collider.enabled = false;
            GameObject.Find("NextLevel").collider.enabled = true;
        }
    }

    private void moveSock()
    {
        //premakni glavo
        transform.position += transform.forward;
        //uniči zadnji člen repa
        Destroy(tail[0]);
        tail.RemoveAt(0);
        //dodaj prvi člen repa
        tail.Add(Instantiate(tail_part, transform.position - transform.forward, Quaternion.identity));
        ((GameObject)tail[tail.Count - 1]).collider.enabled = true;
    }

    public void takeDamage()
    {
        GameManager.lives--;
        if (GameManager.lives <= 0)
            Application.LoadLevel("EndScreen");
        resetLevel();
    }

    private void resetLevel()
    {
        //pozicioniraj na začetek
        transform.position = new Vector3(0.5f, 0.5f, -14.5f);
        //obrni navzgor
        orientation.Direction = Orientation.direction.UP;
        transform.rotation = orientation.getQuaternion();
        //uniči rep
        for (int i = 0; i < tail.Count; i++)
            Destroy(tail[i]);
        tail.Clear();
        //ustvari rep
        for (int i = GameManager.start_length; i > 0; i--)
            tail.Add(Instantiate(tail_part, transform.position - transform.forward * i, Quaternion.identity));
    }
    System.Collections.IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
    void OnTriggerEnter(Collider other)
    {
        //če se zaleti v nasprotnika
        if (other.tag == "Enemy"){
            GameManager.move_speed = 0.0f;
            wait(0.5f);
            takeDamage();
            GameManager.move_speed = 2.0f;
			//igraj zvok
			if(other.GetComponent<AudioSource>() != null){
				other.audio.Play();
			}
			else
				this.GetComponents<AudioSource>()[2].Play();
		}

        //če se zaleti v jabolko
        if (other.tag == "Objective")
        {
            //povečaj točke
            if (other.name.Contains("HroščZlati")){
                GameManager.points += 10;
				//igraj zvok
				this.GetComponents<AudioSource>()[1].Play();
			}
            else{
                GameManager.points++;
				//igraj zvok
				this.GetComponents<AudioSource>()[0].Play();
			}
            //povečaj rep
            tail.Add(Instantiate(tail_part, transform.position - transform.forward, Quaternion.identity));
            //omogoči hitbox predzadnjega člena 
            ((GameObject)tail[tail.Count - 1]).collider.enabled = true;
            //uniči jabolko
            Destroy(other.gameObject);
        }
    }
}
