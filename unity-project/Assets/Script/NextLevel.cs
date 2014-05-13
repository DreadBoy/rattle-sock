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

    IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(2.0f);
        if (other.name == "Head")
			this.audio.Play();  
            loadNextLevel();
    }

    private void loadNextLevel()
    {
        
        if (loadedLevel == 30)
            Application.LoadLevel("EndScreen");
        Application.LoadLevel("level" + loadedLevel);

    }
}
