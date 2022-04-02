using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject player;
    private GameObject changeShipCanvas;
    public bool lastLevel = false;
    private bool playerVisible = true;
    public ParticleSystem particleSystem;
    public GameObject darkCanvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        changeShipCanvas = GameObject.Find("EndLevelMenuCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) playerVisible = player.GetComponent<Renderer>().isVisible;
    }
    public void BossFightEnd()
    {
        StartCoroutine(EndLevelCorountine());
    }
    public IEnumerator EndLevel()
    {
        GameObject.Instantiate(particleSystem).transform.position = player.transform.position;
        yield return new WaitForSeconds(0.2F);
        try { 
        GameObject.Destroy(Camera.main.GetComponent<CameraMovement>()); 
        GameObject.Find("background end").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        } catch (NullReferenceException) { }

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        yield return new WaitUntil(() => playerVisible == false);
        Destroy(changeShipCanvas);
        player.transform.position = new Vector2(0, -2.6F);
        player.GetComponent<PlayerMovement>().MovementActive = true;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<PlayerMovement>().verticalSpeed);
        LoadNextLevel();
    }
    //loads next scene
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator EndLevelCorountine()
    {
        //destroy all enemies and projectiles
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        for(byte b = 0; b < 3; b++)
        {
            foreach (GameObject g in gameObjects)
            {
                if(i % 3 == b)
                {
                    g.GetComponent<HealthManager>().Destroy(true);
                }
                i++;
            }
            yield return new WaitForSeconds(0.1F);
            i = 0;
        }
        //disable player weapons and movement
        player.GetComponent<PlayerWeaponManager>().WeaponsActive = false;
        player.GetComponent<PlayerMovement>().MovementActive = false;
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>().Where(g => g.layer == GlobalVariables.layers[2] || g.layer == GlobalVariables.layers[3])){
            GameObject.Destroy(g);
        }
        //shows change ship menu
        if (!lastLevel)
        {
            changeShipCanvas.SetActive(true);
            changeShipCanvas.GetComponent<CanvasGroup>().interactable = true;
            for (i = 0; i < 10; i++)
            {
                changeShipCanvas.GetComponent<CanvasGroup>().alpha += 0.1F;
                yield return new WaitForSeconds(0.1F);
            }
        }
        else
        {
            //game ends
            GameObject.Find("MainUi").SetActive(false);
            GameObject.Destroy(Camera.main.GetComponent<CameraMovement>());
            GameObject.Find("background end").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(0.5F);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);
            yield return new WaitForSeconds(0.5F);
            GameObject canvas = GameObject.Instantiate(darkCanvas);
            for(int c = 0; c < 10; c++)
            {
                yield return new WaitForSeconds(0.2F);
                canvas.GetComponent<CanvasGroup>().alpha += 0.1F;
            }
            
            SetMenuShip(player.GetComponent<PlayerUpgrade>().shipIndex);
            GameObject.Destroy(player);
            yield return new WaitForSeconds(3F);
            SceneManager.LoadScene(0);
        }
        void SetMenuShip(int ship)
        {
            PlayerPrefs.SetInt("MainMenuShip", ship);
            PlayerPrefs.Save();
        }
    }
}
