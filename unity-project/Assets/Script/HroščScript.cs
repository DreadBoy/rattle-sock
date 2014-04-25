using UnityEngine;
using System.Collections;

public class HroščScript : MonoBehaviour
{
    float interval = 2; //vsaki dve sekundi
    float timer;
    public GameObject zlati_hrosc;

    // Use this for initialization
    void Start()
    {
        timer = Time.time + interval;
    }

    // Update is called once per frame
    void Update()
    {
        //vsaki 2 sekundi(manj zre) je moznost da se eden izmed hroscev spremeni v zlatega

        if (Time.time >= timer)
        {
            if (Random.Range(0.0f, 100.0f) > 90.0f) //random možnost, da se spremeni
            {
                Instantiate(zlati_hrosc, transform.localPosition, new Quaternion()); //ustvari novega zlatega hrošča
                Destroy(transform.gameObject); //odstrani starega navadnega hrošča
            }
            timer = Time.time + interval;
        }
    }
}
