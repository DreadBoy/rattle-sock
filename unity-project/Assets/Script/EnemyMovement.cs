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
	
	Orientation orientation = new Orientation();
	List<Object> breadcrumbs = new List<Object>();
	public GameObject breadcrumb;

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
        if (time_span > 0.2f / speed)
        {
            time_span -= 0.2f / speed;
			int index = 0;
			double cena = 0;
			GameObject[] hroscs = GameObject.FindGameObjectsWithTag("Objective");
			for (int i = 0; i < hroscs.Length; i++) {
				double cena_curr = PathFinder.dobiCeno(hroscs[i].transform.localPosition, transform.localPosition);
				if(cena_curr > cena){
					cena = cena_curr;
					index = i;
				}
			}
			foreach (Object crumb in breadcrumbs) {
				Destroy(crumb);
			}
			breadcrumbs.Clear();
			List<Vector2> path = PathFinder.findPath (
				new Vector2(transform.localPosition.x + 16, transform.localPosition.z + 16),
				new Vector2(hroscs[index].transform.localPosition.x + 16.5f, hroscs[index].transform.localPosition.z + 16.5f));
			foreach (Vector2 node in path) {
				breadcrumbs.Add(Instantiate (breadcrumb, new Vector3(node.x - 16, 1f, node.y - 16), Quaternion.identity));
			}
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

    public void avoidObstacle()
    {
		//zasukaj se v desno, da se izogneš tarči
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
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            avoidObstacle();
        }
    }
}
