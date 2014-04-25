using UnityEngine;
using System.Collections;

public class SpawnHrošč : MonoBehaviour {

    private static GameObject _hrosc;
	public GameObject hrosc;
	static GameObject[] hrosciList;
	static int hroscListCount = 0;
	
	public static void ustvariHrošč()
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
	
	private static void InstantiateHrošč(float x, float z){
		hrosciList[hroscListCount] = (GameObject)Instantiate(_hrosc, new Vector3(x + 0.5f, 0.6f, z + 0.5f), new Quaternion());
		hroscListCount++;
	}
	
	void Awake() {
        _hrosc = hrosc;
		hrosciList = new GameObject[10];
        hroscListCount = 0;
		//pozicioniraj hrosce
		for (int i = 0; i < 10; i++)
		{
			ustvariHrošč();
		}
	}
	

	

}
