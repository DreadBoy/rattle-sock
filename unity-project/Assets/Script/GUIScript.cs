using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public GUISkin RattleSnake;

    void OnGUI()
    {
        GUI.skin = RattleSnake;
        GUI.Label(new Rect(Screen.width - Screen.width / 4, 0, 1000, 1000), "TOČKE: " + Movement.tocke);
    }
}
