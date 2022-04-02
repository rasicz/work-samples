using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartTutorial : LevelStart
{
    protected override void Start()
    {
        Player = GameObject.Find("PlayerShip");

        mainUi = GameObject.Find("MainUi").GetComponent<CanvasGroup>();
        changeShipCanvas = GameObject.Find("EndLevelMenuCanvas");
        mainUi.alpha = 0;

        if (notFirstLevel) Player.GetComponent<PlayerMovement>().MovementActive = true;

        StartCoroutine(GlobalVariables.ChangeCanvasAlpha(mainUi, transparencyTime, true));
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (mainUi.alpha == 1)
        {
            mainUi.alpha = 1;
            Destroy(this);
        }
    }
}
