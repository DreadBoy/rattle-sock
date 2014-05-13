using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    private bool escape_key = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!escape_key)
                GameManager.pause = !GameManager.pause;
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
        menu_style.fontSize = 30;
        menu_style.alignment = TextAnchor.MiddleCenter;

        GUIStyle label_style = new GUIStyle(GUI.skin.label);
        label_style.fontSize = 20;

        GUIStyle button_style = new GUIStyle(GUI.skin.button);
        button_style.fontSize = 20;
        button_style.margin = new RectOffset(0, 0, 20, 0);
        button_style.fixedHeight = 40;

        GUIStyle box_style = new GUIStyle(GUI.skin.box);
        box_style.padding = new RectOffset(15, 15, 15, 15);

        GUI.Label(new Rect(Screen.width / 3, 0,500, 500), GameManager.points.ToString(), točke_style);

        string lifes_display = "";
        for (int i = 0; i < GameManager.lives; i++)
            lifes_display += "★";
        GUI.Label(new Rect(2 * Screen.width / 3, 0, 500, 500), lifes_display, točke_style);

        if (GameManager.pause)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 95, 240, 190), box_style);
            GUILayout.BeginVertical();

            GUILayout.Label("Menu", menu_style);
            if (GUILayout.Button("Main menu", button_style))
            {
                Application.LoadLevel("MainMenu");
            }
            if (GUILayout.Button("New game", button_style))
            {
                GameManager.resetGame();
                Application.LoadLevel(0);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }



        

       
    }
}
