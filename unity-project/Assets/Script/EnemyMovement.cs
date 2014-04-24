using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Object = UnityEngine.Object;
using Assets.Script;

public class EnemyMovement : MonoBehaviour
{
    public Object tail_part;
    private List<Object> tail = new List<Object>();
    private float time_span = 0;

    Orientation orientation = new Orientation();
    List<Object> breadcrumbs = new List<Object>();
    public GameObject breadcrumb;
    List<Vector2> path = new List<Vector2>();

    public bool show_breadcrumbs = false;

    // Use this for initialization
    void Start()
    {
        PathFinder.createMatrix();
        resetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        time_span += Time.deltaTime;
        if (time_span > 0.2f / GameManager.move_speed)
        {
            time_span -= 0.2f / GameManager.move_speed;
            //if (path.Count <= 0)
            //    findTarget();
            //followPath();
            moveSock();
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

    private void findTarget(Vector2 goal = new Vector2())
    {
        int index = -1;
        double cena = 0;
        GameObject[] hroscs = GameObject.FindGameObjectsWithTag("Objective");
        if (hroscs.Length <= 0)
            return;
        for (int i = 0; i < hroscs.Length; i++)
        {
            double cena_curr = PathFinder.dobiCeno(hroscs[i].transform.localPosition, transform.localPosition);
            if (cena_curr > cena)
            {
                cena = cena_curr;
                index = i;
            }
        }
        if (index >= 0)
        {
            if(goal == new Vector2())
                goal = new Vector2(hroscs[index].transform.localPosition.x + 16.5f, hroscs[index].transform.localPosition.z + 16.5f);
            path = PathFinder.findPath(
                new Vector2(transform.localPosition.x + 16, transform.localPosition.z + 16),
                goal);
            foreach (Object crumb in breadcrumbs)
            {
                Destroy(crumb);
            }
            breadcrumbs.Clear();
            //vizualno prikaži pot
            if (show_breadcrumbs)
                foreach (Vector2 node in path)
                {
                    breadcrumbs.Add(Instantiate(breadcrumb, new Vector3(node.x - 15.5f, 1f, node.y - 15.5f), Quaternion.identity));
                }
        }

    }

    private void followPath()
    {
        if (path.Count <= 0)
            return;
        Vector2 localPos = new Vector2(transform.localPosition.x + 16, transform.localPosition.z + 16);
        Vector2 direc = path[0] - localPos;
        path.RemoveAt(0);
        if (direc.x == 0 && direc.y > 0)
            orientation.Direction = Orientation.direction.UP;
        else if (direc.x > 0 && direc.y == 0)
            orientation.Direction = Orientation.direction.RIGHT;
        else if (direc.x == 0 && direc.y < 0)
            orientation.Direction = Orientation.direction.DOWN;
        else if (direc.x < 0 && direc.y == 0)
            orientation.Direction = Orientation.direction.LEFT;
        transform.localRotation = orientation.getQuaternion();
    }
    public void avoidObstacle()
    {
        //zasukaj se v desno, da se izogneš tarči
        orientation.rotateRight();
        transform.localRotation = orientation.getQuaternion();
        //prekliči načrtovano pot in poišči novo
        if(path.Count > 0)
            findTarget(path[path.Count - 1]);
    }

    private void resetPosition()
    {
        //obrni navzgor
        orientation = new Orientation();
        transform.rotation = orientation.getQuaternion();
        //uniči rep
        for (int i = 0; i < tail.Count; i++)
            Destroy(tail[i]);
        tail.Clear();
        //ustvarim rep
        for (int i = GameManager.start_length; i > 0; i--)
            tail.Add(Instantiate(tail_part, transform.position - transform.forward * i, Quaternion.identity));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            avoidObstacle();
        }
        else if (other.tag == "Objective")
        {
            //povečaj rep
            tail.Add(Instantiate(tail_part, transform.position - transform.forward, Quaternion.identity));
            //omogoči hitbox predzadnjega člena 
            ((GameObject)tail[tail.Count - 1]).collider.enabled = true;
            //uniči jabolko
            Destroy(other.gameObject);
        }
    }
}
