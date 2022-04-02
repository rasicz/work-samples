using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer renderer;
    private GameObject player;
    private int phase;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        player = GameObject.Find("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case 0:
                if (renderer.isVisible)
                {
                    Camera.main.GetComponent<CameraMovement>().Difference *= -1;
                    player.GetComponent<PlayerWeaponManager>().WeaponsActive = false;
                    phase++;
                }
                break;
            case 1:
                if(Math.Abs(Camera.main.transform.position.y - Camera.main.GetComponent<CameraMovement>().Difference - player.transform.position.y) < 0.1F)
                {
                    player.GetComponent<PlayerWeaponManager>().RearWeapons = !player.GetComponent<PlayerWeaponManager>().RearWeapons;
                    player.GetComponent<PlayerWeaponManager>().WeaponsActive = true;
                    Destroy(this);
                }
                break;
        }
        
    }
}
