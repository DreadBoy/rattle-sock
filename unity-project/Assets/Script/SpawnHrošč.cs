using UnityEngine;
using System.Collections;

public class SpawnHrošč : MonoBehaviour {
	
	public GameObject hrosc;
	GameObject[] hrosciList;
	int hroscListCount = 0;
	
	float interval = 1; //for 1 second
	float timer;
	
	private void ustvariHrošč()
	{
		bool inside;
		float x;
		float z;
		do{
			inside = false;
			x = Random.Range(15, -14);
			z = Random.Range(15, -14);
			if (PathFinder.createMatrix()[(int)x + 16, (int)z + 16] > 0) //preveri, če je znotraj kakšne ovire
				inside = true;
			for (int j = 0; j < hroscListCount; j++) //preveri, če se prekriva s kakšnim obstoječim
			{
				if ((x == hrosciList[j].transform.localPosition.x) && (z == hrosciList[j].transform.localPosition.z)) //znotraj hrosca
				{
					inside = true;
					break;
				}
			}
		}while(inside);
		InstantiateHrošč(x, z);
	}
	
	private void InstantiateHrošč(float x, float z){
		hrosciList[hroscListCount] = (GameObject)Instantiate(hrosc, new Vector3(x + 0.5f, 0.6f, z + 0.5f), new Quaternion());
		hroscListCount++;
	}
	
	void Awake() {
		hrosciList = new GameObject[10];
		
		//pozicioniraj hrosce
		for (int i = 0; i < 10; i++)
		{
			ustvariHrošč();
		}
	}
	
	// Use this for initialization
	void Start () {
		timer = Time.time + interval;
	}
	
	// Update is called once per frame
	void Update () {
		//vsaki 2 sekundi(manj zre) je moznost da se eden izmed hroscev spremeni v zlatega
		
		timer = Time.time + interval;
		
		if(Time.time >= timer) //if the current time elapsed is equal to or greater than the timer
		{
			if (Random.Range(0.0f, 100.0f) > 10.0f)
			{
				hrosciList[Random.Range(0, hroscListCount)].transform.Translate(new Vector3(0.0f, 5.0f, 0.0f));
			}
			timer = Time.time + interval; //set the timer again
		}
	}
}
