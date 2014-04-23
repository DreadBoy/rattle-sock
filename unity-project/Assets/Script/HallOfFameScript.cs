﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallOfFameScript : MonoBehaviour
{



    void Start()
    {
        /*GameManager.highScores.Add(new GameManager.ScoreEntry
        {
            name = "Matic",
            score = 100
        });*/
    }

    void OnGUI()
    {
        GUIStyle label_style = new GUIStyle(GUI.skin.label);
        label_style.fontSize = 20;
        label_style.alignment = TextAnchor.MiddleCenter;

        GUIStyle menu_style = new GUIStyle(GUI.skin.label);
        menu_style.fontSize = 30;
        menu_style.alignment = TextAnchor.MiddleCenter;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;
        button_style.margin = new RectOffset(0, 0, 10, 0);

        GUIStyle box_style = new GUIStyle(GUI.skin.box);
        box_style.padding = new RectOffset(15, 15, 15, 15);

        int box_height = 80 + 25 * GameManager.highScores.Count + 40 + 20;

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 90, 220, 180), box_style);
        GUILayout.BeginVertical();

        GUILayout.Label("Hall of Fame", menu_style);

        for (int i = 0; i < GameManager.highScores.Count; i++)
            GUILayout.Label(string.Format("{0} : {1:#,0}", GameManager.highScores[i].name, GameManager.highScores[i].score), label_style);

        if (GUILayout.Button("Back", button_style))
        {
            Application.LoadLevel("MainMenu");
        }
        
        GUILayout.EndVertical();
        GUILayout.EndArea();

    }
}
