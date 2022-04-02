using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    
    private GameObject player;
    public byte weapon;
    public float time;

    private PlayerWeaponManager weaponManager;
    private PlayerHealthManager healthManager;
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        weaponManager = player.GetComponent<PlayerWeaponManager>();
        healthManager = player.GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);

        switch (type)
        {
            case 0:
                weaponManager.UnlockWeapon(weapon, time);
                break;
            case 1:
                if(healthManager.health < healthManager.maxHealth) 
                healthManager.health += 1;
                break;
        }
    }
}
