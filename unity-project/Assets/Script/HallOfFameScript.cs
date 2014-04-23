using UnityEngine;
using System.Collections;

public class HallOfFameScript : MonoBehaviour
{

    void OnGUI()
    {
        GUIStyle točke_style = new GUIStyle(GUI.skin.label);
        točke_style.fontSize = 50;

        GUIStyle menu_style = new GUIStyle(GUI.skin.label);
        menu_style.fontSize = 20;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;

        GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 240, 200), new GUIContent());
        GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 80, 120, 40), "Hall of fame", menu_style);
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 20, 120, 40), "New game", button_style))
        {
            GameManager.pause = false;
            Application.LoadLevel(0);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 40, 120, 40), "Hall of fame", button_style))
            Application.LoadLevel("HallOfFame");

    }
}
