﻿using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{

    //kill me, ampak ne da se mi delat GameManagerja
    public static bool pause = false;
    private bool escape_key = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!escape_key)
                pause = !pause;
            escape_key = true;
        }
        else
            escape_key = false;
    }

    void OnGUI()
    {
        GUIStyle točke_style = new GUIStyle(GUI.skin.label);
        točke_style.fontSize = 50;

        GUIStyle menu_style = new GUIStyle(GUI.skin.label);
        menu_style.fontSize = 20;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;

        GUI.Label(new Rect(Screen.width - Screen.width / 4, 0, 1000, 1000), "TOČKE: " + Movement.tocke, točke_style);

        if (pause)
        {
            GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 240, 200), new GUIContent());
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 80, 60, 40), "Menu", menu_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 20, 120, 40), "Main menu", button_style))
                print("Main Menu");
            if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 40, 120, 40), "New game", button_style))
                Application.LoadLevel(0);
        }
    }
}