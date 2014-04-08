using UnityEngine;
using System.Collections;

public class SpawnHroscScript : MonoBehaviour {

    public GameObject hrosc;
    Vector3[] hrosciList;
    int hroscListCount = 0;

    private void pozicioniraj()
    {
        float x = Random.Range(15, -14);
        float z = Random.Range(15, -14);
        x += 0.5f;
        z += 0.5f;

        //ce je znotraj ovire, ali drugega hosca try again
        //znotraj ovire
        /*if( wtf kak naj to preverim?){ */
            //znotraj hrosca
            if(hroscListCount > 0)
            {
                for (int j = 0; j < hroscListCount; j++)
                {
                    if ((x == hrosciList[j].x) && (z == hrosciList[j].z))
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
        /*}*/
    }

	// Use this for initialization
	void Start () {

        hrosciList = new Vector3[10];

	    //pozicioniraj hrosce
        for (int i = 0; i < 10; i++)
        {
            pozicioniraj();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
