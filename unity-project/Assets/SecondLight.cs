using UnityEngine;
using System.Collections;

public class SecondLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject lightGameObject = new GameObject("The Light");
        lightGameObject.AddComponent<Light>();
        lightGameObject.light.color = Color.white;
        lightGameObject.transform.position = new Vector3(15.3f,23.7f,-20.3f);
        lightGameObject.transform.rotation = Quaternion.Euler(new Vector3(50f, 330f, 0f));
        lightGameObject.light.type = LightType.Directional;
        lightGameObject.light.intensity = 0.2f;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
