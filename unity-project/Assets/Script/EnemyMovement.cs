using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Object = UnityEngine.Object;

public class EnemyMovement : MonoBehaviour
{
    public int length = 7;
    public float speed = 1f;
    public Object tail_part;
    private List<Object> tail = new List<Object>();
    private float time_span = 0;
    private Random rand_turn = new Random();
    private struct orientation
    {
        public static Quaternion up = Quaternion.Euler(0, 0, 0);
        public static Quaternion right = Quaternion.Euler(0, 90, 0);
        public static Quaternion down = Quaternion.Euler(0, 180, 0);
        public static Quaternion left = Quaternion.Euler(0, 270, 0);
        public static Quaternion next(Quaternion current)
        {
            if (current == up)
                return orientation.right;
            else if (current == right)
                return orientation.down;
            else if (current == down)
                return orientation.left;
            else if (current == left)
                return orientation.up;
            else
                return orientation.up;
        }
        public static Quaternion random(Quaternion current)
        {
            Random rand = new Random();
            int Y = rand.Next(0, 3);
            while(Y == current.y){
                Y = rand.Next(0, 3);
            }
            return Quaternion.Euler(0, Y*90, 0);
        }
    }

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
            if(rand_turn.Next(0, 10) == 0)
                transform.localRotation = orientation.random(transform.localRotation);
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
        transform.localRotation = orientation.next(transform.localRotation);
    }

    private void resetPosition()
    {
        transform.rotation = orientation.up;
        for (int i = 0; i < tail.Count; i++)
            Destroy(tail[i]);
        tail.Clear();
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
