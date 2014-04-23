using UnityEngine;
using System.Collections;
using System;

public class EndScreenScript : MonoBehaviour
{
    string player_name = "";

    void OnGUI()
    {
        GUIStyle label_style = new GUIStyle(GUI.skin.label);
        label_style.fontSize = 20;

        GUIStyle textfield_style = new GUIStyle(GUI.skin.textField);
        textfield_style.fontSize = 20;

        GUIStyle menu_style = new GUIStyle(GUI.skin.label);
        menu_style.fontSize = 30;
        menu_style.alignment = TextAnchor.MiddleCenter;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;
        button_style.margin = new RectOffset(0, 0, 10, 0);

        GUIStyle box_style = new GUIStyle(GUI.skin.box);
        box_style.padding = new RectOffset(15, 15, 15, 15);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 105, 200, 210), box_style);
        GUILayout.BeginVertical();

        GUILayout.Label("Save score", menu_style);

        GUILayout.Label("Name: ", label_style);
        player_name = GUILayout.TextField(player_name, 19, textfield_style);
        GUILayout.Label("Score: " + GameManager.points, label_style);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("OK", button_style) && !String.IsNullOrEmpty(player_name))
        {
            GameManager.highScoresAdd(new GameManager.ScoreEntry
            {
                name = player_name,
                score = GameManager.points
            });
            Application.LoadLevel("MainMenu");
        }

        if (GUILayout.Button("Cancel", button_style))
        {
            Application.LoadLevel("MainMenu");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
