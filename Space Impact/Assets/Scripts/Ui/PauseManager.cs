using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused;
    public void PauseMenu(bool show)
    {
        if (show)
        {
            GlobalVariables.Paused = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            GlobalVariables.Paused = false;
            pauseMenu.SetActive(false);
        }
    }
    public void TogglePause()
    {
        GlobalVariables.Paused = !GlobalVariables.Paused;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    void OnApplicationFocus(bool pauseStatus)
    {
        if (Time.timeScale == 0) return;
        if (!pauseStatus)
        {
            PauseMenu(true);
        }
    }
}
