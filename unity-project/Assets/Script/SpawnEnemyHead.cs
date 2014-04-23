using UnityEngine;
using System.Collections;

public class SpawnEnemyHead : MonoBehaviour {
	public GameObject enemyHead;
	GameObject[] enemyHeadList;
	int enemyHeadListCount = 0;
	
	private void ustvariEnemyHead(){
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
			for (int j = 0; j < enemyHeadListCount; j++) //preveri, če se prekriva s kakšnim obstoječim
			{
				if ((x == enemyHeadList[j].transform.localPosition.x) && (z == enemyHeadList[j].transform.localPosition.z)) //znotraj hrosca
				{
					inside = true;
					break;
				}
			}
		}while(inside);
		InstantiateEnemyHead(x, z);
	}
	
	private void InstantiateEnemyHead(float x, float z){
		enemyHeadList[enemyHeadListCount] = (GameObject)Instantiate(enemyHead, new Vector3(x + 0.5f, 0.6f, z + 0.5f), new Quaternion());
		enemyHeadListCount++;
	}

	void Awake(){
		enemyHeadList = new GameObject[10];
		
		//pozicioniraj hrosce
		for (int i = 0; i < GameManager.kače_count; i++)
		{
			ustvariEnemyHead();
		}
	}
	
	// Use this for initialization
	void Start () {
	}
}