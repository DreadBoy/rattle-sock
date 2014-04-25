using UnityEngine;
using System.Collections;

public class HroščZlatiScript : MonoBehaviour
{
    float interval = 1; //čez 10 sekund
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
            Destroy(transform.gameObject); //odstrani zlatega hrošča
            SpawnHrošč.ustvariHrošč();
        }
    }
}
