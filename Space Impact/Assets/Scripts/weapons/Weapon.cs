using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public bool enemy;
    [HideInInspector]
    private bool active;
    public bool Active
    {
        get { return active; }
        set
        {
            if (active == value) return;
            active = value;
            if (active && !activated)
            {
                activated = true;
                OnActivated();
            }
        }
    }
    protected bool activated = false;
    [HideInInspector]
    public float lastTimeFired;

    public Vector2[] spawnLocations;
    public GameObject projectile;
    public int projectileDamage;
    public float projectileSpeed;
    public float reloadSpeed;
    [Header("sprite")]
    public int sprite;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    public bool isUsingShield;
    public GameObject shield;
    public bool exclusive;
    private int modulo = 6;
    void Start()
    {
        lastTimeFired += Time.time - (reloadSpeed * 0.5F);
        if(isUsingShield) shield.GetComponent<Shield>().parent = gameObject;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!enemy)
        {
            if (modulo == 2) modulo = 4;
            else modulo = 2;
        }
        else
        {
            modulo = Time.frameCount % 6;
        }
        switch (modulo)
        {
            case 2:
                if (reloadSpeed == -1) break;
                if (Active && Time.time >= lastTimeFired + reloadSpeed)
                {
                    useWeapon();
                    lastTimeFired = Time.time;
                }
                break;
            case 4:
                if(!active && activated)
                {
                    if(isUsingShield) shield.SetActive(false);
                    activated = false;
                }
                break;
        }
    }
    virtual protected void useWeapon()
    {
    }
    virtual protected void OnActivated()
    {
        if (isUsingShield) shield.SetActive(true);
        if (reloadSpeed == -1) useWeapon(); 
    }
}
