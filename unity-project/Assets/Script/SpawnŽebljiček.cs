using UnityEngine;
using System.Collections;

public class SpawnŽebljiček : MonoBehaviour {
	
	float hitrost = 10.0f;
	Vector3 smer = new Vector3(1.0f, 0.0f, 1.0f);
	
	public GameObject žebljiček;
	GameObject[] žebljičekList;
	int žebljičekListCount = 0;
	
	void OnCollisionEnter(Collision collision)
	{
		//bounce
	}
	
	private void ustvariŽebljiček(){
		bool inside;
		float x;
		float z;
		do{
			inside = false;
			x = Random.Range(15, -14);
			z = Random.Range(15, -14);
			if (PathFinder.createMatrix()[(int)x + 16, (int)z + 16] > 0) //preveri, če je znotraj kakšne ovire
				inside = true;
			if( -5 < x && x < 5 && -14 < z && z < -4) //preveri, če je preblizu spawna
				inside = true;
			for (int j = 0; j < žebljičekListCount; j++) //preveri, če se prekriva s kakšnim obstoječim
			{
				if ((x == žebljičekList[j].transform.localPosition.x) && (z == žebljičekList[j].transform.localPosition.z)) //znotraj hrosca
				{
					inside = true;
					break;
				}
			}
		}while(inside);
		InstantiateŽebljiček(x, z);
	}
	
	private void InstantiateŽebljiček(float x, float z){
		žebljičekList[žebljičekListCount] = (GameObject)Instantiate(žebljiček, new Vector3(x + 0.5f, 0.6f, z + 0.5f), new Quaternion());
		žebljičekList[žebljičekListCount].rigidbody.velocity = smer * hitrost;
		žebljičekListCount++;
		
	}
	
	private void pozicioniraj()
	{
		transform.position = new Vector3(Random.Range(15.5f, -14.5f), 0.6f, Random.Range(15.5f, -14.5f));
		//ce je pozicija v centru (nocemo umret takoj na zacetku igre)
		if ((transform.position.x > -6.5f && transform.position.x < 6.5f) || (transform.position.z > -9.0f && transform.position.z < 9.0f))
		{
			//fakn'rekurzija, SON!
			pozicioniraj();
			
			//daj zebljicku hitrost
			rigidbody.velocity = smer * hitrost;
		}
	}
	
	void Awake(){
		žebljičekList = new GameObject[10];
		
		//pozicioniraj hrosce
		for (int i = 0; i < GameManager.žebljiček_count; i++)
		{
			ustvariŽebljiček();
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//vrtenje okrog osi
		for (int i = 0; i < žebljičekListCount; i++)
		{
			žebljičekList[i].transform.Rotate(new Vector3(0.0f, 0.0f, 10.0f));
		}
	}
}