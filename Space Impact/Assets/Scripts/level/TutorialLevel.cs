using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevel : MonoBehaviour
{
    // Start is called before the first frame update
    int phase = 0;
    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject controlsHelpCanvas;
    public GameObject specialAttackCanvas;
    public GameObject mainMenu;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        player.GetComponent<PlayerWeaponManager>().WeaponsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case 0:
                //TO DO: change to be % of aspect ratio
                if (math.abs(player.transform.position.x) > 2) {
                    StartCoroutine(GlobalVariables.ChangeCanvasAlpha(controlsHelpCanvas.GetComponent<CanvasGroup>(), 1, false));
                    phase++;
                } 
                break;
            case 1:
                //removes control view and spawns enemies
                Vector2 spawnerPosition = new Vector2(0, Camera.main.orthographicSize + Camera.main.transform.position.y + 5);
                if (enemySpawner != null) GameObject.Instantiate(enemySpawner).transform.position = spawnerPosition;
                phase++;
                //phase = 10;
                break;
            case 2:
                if (GameObject.Find("enemySpawnerTutorial 1(Clone)") != null) break;
                StartCoroutine(EnableWeapons());
                phase++;
                break;
            case 3:
                if (GameObject.Find("enemySpawnerTutorial 1(Clone)") != null || GameObject.Find("EnemyShip1 tutorial(Clone)") != null) break;
                player.GetComponent<PlayerWeaponManager>().activeWeapon[0] = false;
                player.GetComponent<PlayerWeaponManager>().weapons[0].Active = false;
                GameObject.Instantiate(enemySpawner2).transform.position = new Vector2(0, Camera.main.orthographicSize + Camera.main.transform.position.y + 1);
                StartCoroutine(EnableSpecialAttack());
                phase++;
                //phase = 10;
                //phase++;
                break;
            case 4:
                if (player.GetComponent<PlayerWeaponManager>().activeWeapon[2])
                {
                    specialAttackCanvas.SetActive(false);
                    GlobalVariables.Paused = false;
                    phase++;
                }
                break;
            case 5:
                if (GameObject.Find("enemySpawnerTutorial 2(Clone)") != null || GameObject.Find("EnemyShip1 tutorial(Clone)") != null) break;
                player.GetComponent<PlayerWeaponManager>().WeaponsActive = false;
                phase = 10;
                break;
            case 10:
                //ends tutorial
                //DestroyImmediate(player);
                changeTutorialPreferences();
                StartCoroutine(gameObject.GetComponent<LevelEnd>().EndLevel()); //LoadFirstLevel();
                player.GetComponent<PlayerMovement>().MovementActive = false;
                phase++;
                break;
        }
    }
    IEnumerator EnableWeapons()
    {
        yield return new WaitForSeconds(2F);
        player.GetComponent<PlayerWeaponManager>().WeaponsActive = true;
    }
    IEnumerator EnableSpecialAttack()
    {
        yield return new WaitForSeconds(3F);
        specialAttackCanvas.SetActive(true);
        mainMenu.SetActive(true);
        GlobalVariables.Paused = true;
    }
    private void OnDestroy()
    {
        DestroyImmediate(player);
    }
    private readonly string tutorialKey = "tutorial";
    public void changeTutorialPreferences()
    {
        PlayerPrefs.SetInt(tutorialKey, 0);
        PlayerPrefs.Save();
    }
    public int startScene;
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(startScene);
    }
}
