using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShipUi : MonoBehaviour
{
    GameObject ScriptHolder;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        ScriptHolder = GameObject.Find("ScriptHolder");
        int[] upgradeIndex = GameObject.Find("PlayerShip").GetComponent<PlayerUpgrade>().shipUpgradeIndex;
        int i = 0;
        foreach (Button b in buttons) { 
            b.image.sprite = ScriptHolder.GetComponent<SpriteHolder>().playerShips[upgradeIndex[i]].GetComponent<SpriteRenderer>().sprite;
            i++;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //changes player ship and ends level
    public void ChangeShip(int index)
    {
        GameObject currentShip = GameObject.Find("PlayerShip");
        int shipIndex = currentShip.GetComponent<PlayerUpgrade>().shipUpgradeIndex[index];
        GameObject newShip = ScriptHolder.GetComponent<SpriteHolder>().playerShips[shipIndex];

        Vector3 position = currentShip.transform.position;
        GameObject.Destroy(currentShip);
        GameObject ship = GameObject.Instantiate(newShip);
        ship.name = "PlayerShip";
        ship.transform.position = position;
        ship.GetComponent<PlayerMovement>().MovementActive = false;
        Camera.main.GetComponent<CameraMovement>().player = ship;

        ScriptHolder.GetComponent<LevelEnd>().player = ship;
        StartCoroutine(ScriptHolder.GetComponent<LevelEnd>().EndLevel());
    }
}
