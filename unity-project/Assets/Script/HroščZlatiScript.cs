using UnityEngine;
using System.Collections;

public class HroščZlatiScript : MonoBehaviour
{
    float interval = 3; //čez 3 sekunde
    float timer;
    // Use this for initialization
    void Start()
    {
        timer = Time.time + interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            SpawnHrošč.respawnZlatiHrošč(transform.gameObject);
        }
    }
}
