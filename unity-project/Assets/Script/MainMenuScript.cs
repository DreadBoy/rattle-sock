using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    void OnGUI()
    {
        GUIStyle menu_style = new GUIStyle(GUI.skin.label);
        menu_style.fontSize = 30;
        menu_style.alignment = TextAnchor.MiddleCenter;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;
        button_style.margin = new RectOffset(0, 0, 20, 0);
        button_style.fixedHeight = 40;

        GUIStyle box_style = new GUIStyle(GUI.skin.box);
        box_style.padding = new RectOffset(15, 15, 15, 15);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 95, 240, 190), box_style);
        GUILayout.BeginVertical();

        GUILayout.Label("Menu", menu_style);
        if (GUILayout.Button("New game", button_style))
        {
            GameManager.resetGame();
            Application.LoadLevel(0);
        }
        if (GUILayout.Button("Hall of fame", button_style))
        {
            Application.LoadLevel("HallOfFame");
        }

        GUILayout.EndVertical();
        GUILayout.EndArea(); 
    }
}
