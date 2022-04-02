using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    protected CanvasGroup mainUi;
    protected GameObject changeShipCanvas;
    public float transparencyTime = 1;
    public bool notFirstLevel;
    public float shipsStartSpeed = 0;
    protected GameObject Player;
    void OnEnable()
    {
        GlobalVariables.shipsStartSpeed = shipsStartSpeed;
    }
    protected virtual void Start()
    {
        Player = GameObject.Find("PlayerShip");

        mainUi = GameObject.Find("MainUi").GetComponent<CanvasGroup>();
        changeShipCanvas = GameObject.Find("EndLevelMenuCanvas");
        mainUi.alpha = 0;
        changeShipCanvas.GetComponent<CanvasGroup>().alpha = 0;
        //changeShipCanvas.SetActive(false);

        if (notFirstLevel) Player.GetComponent<PlayerMovement>().MovementActive = true;

        StartCoroutine(GlobalVariables.ChangeCanvasAlpha(mainUi, transparencyTime, true));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (mainUi.alpha == 1)
        {
            mainUi.alpha = 1;
            Player.GetComponent<PlayerWeaponManager>().WeaponsActive = true;
            Player.GetComponent<PlayerMovement>().MovementActive = true;
            Destroy(this);
        }
    }
}
