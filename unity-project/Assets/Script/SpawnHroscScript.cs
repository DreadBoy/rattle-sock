using UnityEngine;
using System.Collections;

public class SpawnHroscScript : MonoBehaviour {

    public GameObject hrosc;
    GameObject[] hrosciList;
    int hroscListCount = 0;
    float timeInst;
    bool timeGot = false;

    private void pozicioniraj()
    {
        float x = Random.Range(15, -14);
        float z = Random.Range(15, -14);
        x += 0.5f;
        z += 0.5f;

        //ce je znotraj ovire, ali drugega hosca try again
        //znotraj ovire
        if (PathFinder.createMatrix()[(int)x + 15, (int)z + 15] > 0)
        {
            pozicioniraj();
        }
        else
        {
            //znotraj hrosca
            if (hroscListCount > 0)
            {
                for (int j = 0; j < hroscListCount; j++)
                {
                    if ((x == hrosciList[j].transform.position.x) && (z == hrosciList[j].transform.position.z))
                    {
                        //znotraj hrosca
                        pozicioniraj();
                    }
                    else
                    {
                        Instantiate(hrosc, new Vector3(x, 0.6f, z), new Quaternion());
                    }
                }
            }
            else
            {
                Instantiate(hrosc, new Vector3(x, 0.6f, z), new Quaternion());
            }
        }
    }

	// Use this for initialization
	void Start () {

        hrosciList = new GameObject[10];

	    //pozicioniraj hrosce
        for (int i = 0; i < 10; i++)
        {
            pozicioniraj();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    //vsaki 2 sekundi(manj zre) je moznost da se eden izmed hroscev spremeni v zlatega

        /*
        float currentTime = Time.realtimeSinceStartup;
        if (!timeGot)
        {
            timeInst = Time.realtimeSinceStartup;
            timeGot = true;
        }
        
        if (timeInst - currentTime < -2.0f)
        {
            //20% chance
            if (Random.Range(0.0f, 100.0f) > 10.0f)
            {
                hrosciList[Random.Range(0, hroscListCount)].transform.Translate(new Vector3(0.0f, 5.0f, 0.0f));
            }
            timeGot = false;
        }*/
	}
}
