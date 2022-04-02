using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossAi : EnemyAi
{
    byte phase = 0;
    public byte[] specialWeaponReloadSpeed;
    public byte[] specialWeaponUseLenght;
    [Tooltip("special event will be activated when health drops below defined value")]
    public int[] specialEventsOnHealthDrop;
    public enum SpecialEventsTypes { Spawn, SpecialWeapon };
    public SpecialEventsTypes[] specialEventsTypes;
    protected int[] SpecialEventsCounter = new int[2];
    private float[] specialWeaponLastTimeUse;
    int nextSpecialEvent = 0;
    byte activeWeapon = 0;
    HealthManager healthManager;
    [Header("objects spawned during fight")]
    public GameObject[] definedSpawnObjects;
    public GameObject[] randomSpawnObjects;
    protected GameObject scriptHolder;
    public Weapon[] specialWeapons;
    public bool weaponsActive;
    public bool dontEndLevelAfterDestroy = false;

    new void Start()
    {
        specialWeaponLastTimeUse = new float[weapons.Length - 1];        
        foreach(Weapon w in weapons)
        {
            w.enemy = true;
        }
        foreach(Weapon w in specialWeapons)
        {
            w.enemy = true;
        }
        if (specialEventsOnHealthDrop.Length > 0) nextSpecialEvent = 0;
        healthManager = gameObject.GetComponent<HealthManager>();
        healthManager.health = (int)(healthManager.health * GlobalVariables.bossHealth);
        scriptHolder = GameObject.Find("ScriptHolder");

        base.Start();
    }


    new void Update()
    {
        if(healthManager.health == 0)
        {
            weapons.ToList().ForEach(w => w.Active = false);
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            return;
        }

        if (renderer.isVisible) active = true; else return;
        
        switch (phase)
        {
            case 0:
                Ai();
                break;
            case 1:
                weapons[0].Active = true;
                weaponsActive = true;
                phase++;
                break;
            case 2:
                if (weaponsActive && activeWeapon != 0 && specialWeaponLastTimeUse[activeWeapon - 1] + specialWeaponUseLenght[activeWeapon - 1] < Time.time)
                {
                    weapons[activeWeapon].Active = false;
                    weapons[0].Active = true;
                    activeWeapon = 0;
                }
                if (Math.Abs(rigidbody.transform.position.x) >= 2)
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                if (Time.frameCount % 30 == 0 && random.Next(0, 6) == 0)
                {
                    ChoseAction();
                }
                if(specialEventsOnHealthDrop.Length > nextSpecialEvent && healthManager.health <= specialEventsOnHealthDrop[nextSpecialEvent])
                {
                    SpecialEvent();
                    if (specialEventsOnHealthDrop.Length >= nextSpecialEvent)
                    {
                        nextSpecialEvent += 1;
                    }
                }
                if (randomSpawnObjects.Length > 0 && Time.frameCount % 120 == 75 && random.Next(0, 3) == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length <= 8)
                {
                    GameObject.Instantiate(randomSpawnObjects[random.Next(0, randomSpawnObjects.Length)]).transform.position = new Vector2(0, transform.position.y + 2);
                }
                break;
            default:
                break;
        }
    }
    new void Ai()
    {
        Vector2 velocity = new Vector2();
        if (startTime == 0) { startTime = Time.time; }

        if (Time.time >= startTime + TimeBeforeStaying)
        {
            velocity.y = GlobalVariables.playerVerticalSpeed;
            phase += 1;
        }
        rigidbody.velocity = velocity;
    }
    void RandomMove()
    {
        float velocityX;
        if (random.Next(0, 2) == 0)
        {
            velocityX = 0;
        }else if (gameObject.transform.position.x < -1)
        {
            velocityX = 1;
        }else if(gameObject.transform.position.x > 1)
        {
            velocityX = -1;
        }
        else
        {
            velocityX = (random.Next(0, 2) == 0) ? -1 : 1;
        }
        rigidbody.velocity = new Vector2(velocityX, rigidbody.velocity.y);
    }
    void ChoseAction()
    {
        //choses if either gonna move or gonna use special weapon
        if (Math.Abs(rigidbody.transform.position.x) >= 2) {
            RandomMove();
            return;
        }
        if(random.Next(0, 2) == 0)
        {
            RandomMove();
        }
        else
        {
            if(weaponsActive) ChangeWeapon();
        }
    }
    void ChangeWeapon()
    {
        //changes active weapon for certain amount of time
        if(weapons[0].Active == true)
        {
            List<int> available = new List<int>();
            for(int i = 0; i < weapons.Length - 1; i++)
            {
                if (specialWeaponLastTimeUse[i] + specialWeaponUseLenght[i] + specialWeaponReloadSpeed[i] < Time.time) available.Add(i);
            }
            if(available.Count > 0)
            {
                byte chosenWeapon = (byte)random.Next(0, available.Count);
                chosenWeapon = (byte)available[chosenWeapon];
                activeWeapon = (byte)(chosenWeapon + 1);
                specialWeaponLastTimeUse[chosenWeapon] = Time.time;
                weapons[0].Active = false;
                weapons[chosenWeapon + 1].Active = true;
            }
        }
    }
    //makes sure OnDestroy function is not runned when application stops
    void OnApplicationQuit()
    {
        GameObject.DestroyImmediate(this);
    }
    //special event runned when health drops bellow predefined number
    protected void SpecialEvent()
    {
        switch (specialEventsTypes[nextSpecialEvent])
        {
            //spawns enemies or enemy spawner
            case SpecialEventsTypes.Spawn:
                GameObject.Instantiate(definedSpawnObjects[SpecialEventsCounter[0]]).transform.position = new Vector2(0, transform.position.y + 2);
                SpecialEventsCounter[0]++;
                break;
            //uses special weapon
            case SpecialEventsTypes.SpecialWeapon:
                specialWeapons[SpecialEventsCounter[1]].Active = true;
                SpecialEventsCounter[1]++;
                break;
        }
    }
    //makes sure EndLevel function is runned before it gets destroyed
    void OnDestroy()
    {
        if (!dontEndLevelAfterDestroy) 
        scriptHolder.GetComponent<LevelEnd>().BossFightEnd();       
    }
}
