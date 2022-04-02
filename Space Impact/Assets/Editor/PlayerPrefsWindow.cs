using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsWindow : EditorWindow
{
    // Start is called before the first frame update
    static PlayerPrefsWindow playerPrefsWindow;
    [MenuItem("Window/Tools/Player Preferences")]
    static void Init()
    {
        playerPrefsWindow = EditorWindow.GetWindow(typeof(PlayerPrefsWindow)) as PlayerPrefsWindow;
    }
    int menuShip = 0;
    private void OnGUI()
    {
        Vector2 startPosition = new Vector2(5, 5);
        if (GUI.Button(new Rect(startPosition, new Vector2(200, 20)), "reset playerPrefs")) PlayerPrefs.DeleteAll();
        startPosition += new Vector2(0, 25);
        menuShip = EditorGUI.IntField(new Rect(startPosition, new Vector2(200, 20)), menuShip);
        startPosition += new Vector2(0, 25);
        if (GUI.Button(new Rect(startPosition, new Vector2(200, 20)), "set menuShip")) SetMenuShip(menuShip);
    }
    private void SetMenuShip(int ship)
    {
        PlayerPrefs.SetInt("MainMenuShip", ship);
        PlayerPrefs.Save();
    }
}
