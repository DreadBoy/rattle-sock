using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateMatrix : MonoBehaviour {
    public Object breadcrumb;
	public Object Amatrix_prefab;
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
			for (int x = (int)pos.x; x <  (int)pos.x + (int)sca.x; x++) {
				for (int z = (int)pos.z - Mathf.FloorToInt(sca.z/2); z < (int)pos.z + Mathf.CeilToInt(sca.z/2); z++) {
					Amatrix[x+16, z+16] = 1;
				}
			}
		}

        /*for (int x = 0; x < 32; x++)
            for (int y = 0; y < 32; y++)
                if(Amatrix[x, y] <= 0)
                    Instantiate (Amatrix_prefab, new Vector3(x - 16, 0.5f, y - 16), Quaternion.identity);
*/
		/* List<Vector2> path = PathFinder.findPath (new Vector2 (1, 1), new Vector2 (12, 18));
        List<Object> breadcrumbs = new List<Object>();
        foreach (Vector2 node in path) {
            print (node.x + " " + node.y);
            //breadcrumbs.Add(Instantiate (breadcrumb, new Vector3(node.x - 16, 1f, node.y - 16), Quaternion.identity));
			//((GameObject)breadcrumbs[breadcrumbs.Count - 1]).transform.parent = GameObject.Find ("Level").transform;
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
