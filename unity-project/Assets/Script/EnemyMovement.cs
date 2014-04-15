using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Object = UnityEngine.Object;
using Assets.Script;

public class EnemyMovement : MonoBehaviour
{
    public int length = 7;
    public float speed = 1f;
    public Object tail_part;
    private List<Object> tail = new List<Object>();
    private float time_span = 0;
    private Random rand_turn = new Random();
	
	Orientation orientation = new Orientation();

    // Use this for initialization
    void Start()
    {
        resetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        time_span += Time.deltaTime;
        if (time_span > 0.2f / speed)
        {
            time_span -= 0.2f / speed;
            if(rand_turn.Next(0, 10) == 0){
                //orientation.random();
				transform.localRotation = orientation.getQuaternion();
			}
            else
                moveSock();
        }
    }

    private void moveSock()
    {
        transform.position += transform.forward;
        Destroy(tail[0]);
        tail.RemoveAt(0);
        tail.Add(Instantiate(tail_part, transform.position - transform.forward, Quaternion.identity));
        ((GameObject)tail[tail.Count - 1]).collider.enabled = true;
    }

    public void avoidObstacle()
    {
		orientation.rotateRight();
		transform.localRotation = orientation.getQuaternion ();
    }

    private void resetPosition()
    {
        //obrni navzgor
		orientation = new Orientation ();
		transform.rotation = orientation.getQuaternion ();
        //uniči rep
        for (int i = 0; i < tail.Count; i++)
            Destroy(tail[i]);
        tail.Clear();
        //ustvarim rep
        for (int i = length; i > 0; i--)
            tail.Add(Instantiate(tail_part, transform.position - transform.forward * i, Quaternion.identity));
    }

    void OnTriggerEnter(Collider other)
    {
        print("Collide");
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            print("Avoiding");
            avoidObstacle();
        }
    }
}
