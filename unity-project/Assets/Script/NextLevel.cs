using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Head")
            loadNextLevel();
    }

    private void loadNextLevel()
    {
        Application.LoadLevel("level2");
    }
}
