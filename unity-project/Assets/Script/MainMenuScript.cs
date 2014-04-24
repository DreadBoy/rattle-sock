using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    GUIContent[] comboBoxList;
    private ComboBox comboBoxControlSnakes = new ComboBox();
    private ComboBox comboBoxControlPins = new ComboBox();

    private void Start()
    {
        comboBoxList = new GUIContent[3];
        comboBoxList[0] = new GUIContent("0");
        comboBoxList[1] = new GUIContent("1");
        comboBoxList[2] = new GUIContent("2");

        comboBoxControlSnakes = new ComboBox(comboBoxList[2], comboBoxList);
        comboBoxControlPins = new ComboBox(comboBoxList[2], comboBoxList);
    }

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

        GUIStyle label_style = new GUIStyle(GUI.skin.label);
        label_style.fontSize = 20;

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 95, 240, 370), box_style);
        GUILayout.BeginVertical();

        GUILayout.Label("Menu", menu_style);
        if (GUILayout.Button("New game", button_style))
        {
            GameManager.resetGame();
            Application.LoadLevel("level1");
        }
        if (GUILayout.Button("Hall of fame", button_style))
        {
            Application.LoadLevel("HallOfFame");
        }

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Snakes:", label_style);
        GameManager.kače_count = comboBoxControlSnakes.Show();

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.Label("Pins:", label_style);
        GameManager.žebljiček_count = comboBoxControlPins.Show();

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Quit", button_style))
        {
            Application.Quit();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
