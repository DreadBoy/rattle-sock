using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class NextLevel : MonoBehaviour {

    static int loadedLevel;
	// Use this for initialization
	void Start () {
        loadedLevel = Application.loadedLevel;
        loadedLevel++;
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
        if (loadedLevel == 30)
            Application.LoadLevel("EndScreen");
        Application.LoadLevel("level" + loadedLevel);

    }
}
