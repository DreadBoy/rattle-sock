using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateMatrix : MonoBehaviour {

	public int[,] Amatrix = new int[33, 33];
	// Use this for initialization
	void Start () {
		//dodaj stene
		for (int x = 0; x < 32; x++) {
			Amatrix[x, 0] = 1;
			Amatrix[x, 32] = 1;
		}
		for (int y = 0; y < 32; y++) {
			Amatrix[0, y] = 1;
			Amatrix[32, y] = 1;
		}
		//dodaj ovire
		foreach (Transform child in GameObject.Find ("Ovire").transform)
		{
			Vector3 pos = child.localPosition;
			Vector3 sca = child.localScale;
			for (int x = (int)pos.x; x < (int)sca.x; x++) {
				for (int y = (int)pos.y; y < (int)sca.y; y++) {
					Amatrix[x+16, y+16] = 1;
				}
			}
		}
		List<Vector2> path = PathFinder.findPath (new Vector2 (1, 1), new Vector2 (31, 31), Amatrix);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
