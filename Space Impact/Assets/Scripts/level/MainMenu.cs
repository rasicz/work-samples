using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject background;
    GameObject ship;
    public float offsetSpeed;
    AsyncOperation op;
    public GameObject difficultyCanvas;
    public int startScene;
    public int tutorialScene;
    public enum MenuType
    {
        main,
        difficulty,
        credits,
        ingame,
    }
    public MenuType menuType;
    void Start()
    {
        background = GameObject.Find("Background");
        Rigidbody2D component;
        if(background != null && background.TryGetComponent<Rigidbody2D>(out component)) component.velocity = new Vector2(0, - offsetSpeed);
        ship = GameObject.Find("Ship");
        if (menuType == MenuType.main) { 
            difficultyCanvas.SetActive(false);
            int shipID = GetMenuShip();
            ship.GetComponent<SpriteRenderer>().sprite = GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>().playerShips[shipID].GetComponent<SpriteRenderer>().sprite;
        }
    }
    public static int GetMenuShip()
    {
        if (!PlayerPrefs.HasKey("MainMenuShip")) return 0;
        else return PlayerPrefs.GetInt("MainMenuShip");
    }
    void Update()
    {
        if (background == null) return;
        if (background.transform.position.y < -50) background.transform.position = new Vector2(0,50);
        //if (ship.transform.position.y >= -2.6F) op.allowSceneActivation = true;
        GlobalVariables.backgroundOffset = background.transform.position.y % 4;
    }
    public void MenuChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                difficultyCanvas.SetActive(true);
                gameObject.transform.parent.gameObject.SetActive(false);
                break;
            case 1:
                break;
        }

        //op.allowSceneActivation = true;
        //ship.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
        //GlobalVariables.ChangeScene(scene);
    }
    public void StartGame(int difficulty)
    {
        GlobalVariables.GameDifficulty = difficulty;
        Debug.Log("tutorial played? " + tutorialAlreadyPlayed());
        //determines which level is going to load
        if (tutorialAlreadyPlayed())
            op = SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Single);
        else 
            op = SceneManager.LoadSceneAsync(tutorialScene, LoadSceneMode.Single);
    }
    readonly string tutorialKey = "tutorial";
    public bool tutorialAlreadyPlayed()
    {
        if (PlayerPrefs.HasKey(tutorialKey))
        {
            if (PlayerPrefs.GetInt(tutorialKey) == 0)
            {
                return true;
            }
        }
        else
        {
            PlayerPrefs.SetInt(tutorialKey, 1);
            PlayerPrefs.Save();
        }
        return false;
    }
    public void ExitToMainMenu()
    {
        Destroy(GameObject.Find("PlayerShip"));
        SceneManager.LoadScene(0);
        GlobalVariables.Paused = false;
    }
}
