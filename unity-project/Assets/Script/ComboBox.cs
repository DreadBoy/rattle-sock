using UnityEngine;
using System.Collections;

public class ComboBox
{
    private static bool forceToUnShow = false;
    private static int useControlID = -1;
    private bool isClickedComboButton = false;
    private int selectedItemIndex = 2;

    private GUIContent buttonContent;
    private GUIContent[] listContent;
    private GUIStyle buttonStyle;
    private GUIStyle listStyle;

    public ComboBox() { }

    public ComboBox(GUIContent buttonContent, GUIContent[] listContent)
    {
        this.buttonContent = buttonContent;
        this.listContent = listContent;
    }

    public int Show()
    {
        this.buttonStyle = new GUIStyle(GUI.skin.button);

        this.listStyle = new GUIStyle(GUI.skin.box);
        this.listStyle.normal.textColor = Color.white;
        this.listStyle.onHover.background =
        this.listStyle.hover.background = new Texture2D(2, 2);
        this.listStyle.padding = new RectOffset(0, 0, 0, 0);

        if (forceToUnShow)
        {
            forceToUnShow = false;
            isClickedComboButton = false;
        }

        bool done = false;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.mouseUp:
                {
                    if (isClickedComboButton)
                    {
                        done = true;
                    }
                }
                break;
        }

        if (GUILayout.Button(buttonContent, buttonStyle))
        {
            if (useControlID == -1)
            {
                useControlID = controlID;
                isClickedComboButton = false;
            }

            if (useControlID != controlID)
            {
                forceToUnShow = true;
                useControlID = controlID;
            }
            isClickedComboButton = true;
        }

        if (isClickedComboButton)
        {
            int newSelectedItemIndex = GUILayout.SelectionGrid(selectedItemIndex, listContent, 1, listStyle);
            if (newSelectedItemIndex != selectedItemIndex)
            {
                selectedItemIndex = newSelectedItemIndex;
                buttonContent = listContent[selectedItemIndex];
            }
        }

        if (done)
            isClickedComboButton = false;

        return selectedItemIndex;
    }

    public int SelectedItemIndex
    {
        get
        {
            return selectedItemIndex;
        }
        set
        {
            selectedItemIndex = value;
        }
    }
}
