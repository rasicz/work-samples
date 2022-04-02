using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AdvancedEnemyAi : EnemyAi
{
    // Start is called before the first frame update
    public enum MovementPattern { Random, Follow, Flee }
    public MovementPattern movementPattern;
    protected int phase;
    public float horizontalSpeed = 2;
    [Tooltip("chance in %, 0 - 100, defualt is 0")]
    public float defectChange;
    protected GameObject player;
    protected int defectTimer;
    new void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        weapons[0].enemy = true;
        weapons[0].lastTimeFired += random.Next(0, 20) / 10F;
        foreach (Weapon w in weapons)
        {
            w.reloadSpeed *= GlobalVariables.reloadSpead;
        }
        player = GameObject.Find("PlayerShip");
    }

    // Update is called once per frame
    new void Update()
    {
        if (renderer.isVisible) active = true;
        if (active)
        {
            gameObject.GetComponent<HealthManager>().enabled = true;
            weapons[0].Active = true;
            Ai();
        }
    }
    new void Ai()
    {
        Vector2 velocity = rigidbody.velocity;

        switch (phase)
        {
            case 0:
                startTime = Time.time;
                phase++;
                break;
            case 1:
                if (Time.time >= startTime + TimeBeforeStaying)
                {
                    velocity.y = GlobalVariables.playerVerticalSpeed;
                    phase++;
                }
                else
                {
                    if(velocity.y == 0) velocity = new Vector2(0, GlobalVariables.shipsStartSpeed);
                }
                break;
            case 2:
                if(StayOnSceneTime > 0 && Time.time >= startTime + StayOnSceneTime)
                    {
                        if (transform.position.x > 0) velocity.x = 2;
                        else velocity.x = -2;

                        if (!renderer.isVisible)
                        {
                            GameObject.Destroy(gameObject);
                        }
                    }
                else
                {
                    velocity = AdvancedAi(velocity, movementPattern);
                }
                break;
        }                          
        rigidbody.velocity = velocity;
    }
    protected Vector2 AdvancedAi(Vector2 velocity, MovementPattern movementPattern)
    {
        //movement
        switch (movementPattern)
        {
            //randomly set's and changes direction
            case MovementPattern.Random :
                if (math.abs(transform.position.x) >= GlobalVariables.playAreaSize)
                {
                    velocity.x = 0;
                }
                if (Time.frameCount % 10 == 7 && random.Next(0, 5) == 4)
                {
                    if (random.Next(0, 3) == 0)
                    {
                        if (Math.Abs(transform.position.x) >= 2)
                        {
                            velocity.x = transform.position.x > 0 ? horizontalSpeed * -1 : horizontalSpeed;
                        }
                        else
                        {
                            velocity.x = random.Next(0, 2) == 0 ? horizontalSpeed : horizontalSpeed * -1;
                        }
                    }
                    else
                    {
                        velocity.x = 0;
                    }
                }
                break;
            //follows player's position
            case MovementPattern.Follow :
                if (defectTimer > 0)
                {
                    if (Time.frameCount % 5 == 1)
                        defectTimer--;
                }
                else
                {
                    //if defect ocures, ships stops following player for certain time
                    if (Time.frameCount % 7 == 3 && defectChange != 0 && random.Next(0, 101) <= defectChange)
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            velocity.x = 0;
                        }
                        defectTimer = 10;
                    }
                    else
                    {
                        if (math.abs(transform.position.x - player.transform.position.x) < 0.4)
                        {
                            velocity.x = 0;
                        }
                        else
                        {
                            velocity.x = player.transform.position.x > transform.position.x ? horizontalSpeed : horizontalSpeed * -1;
                        }
                    }
                }
                if (math.abs(transform.position.x) >= GlobalVariables.playAreaSize)
                {
                    velocity.x = 0;
                }
                break;
            case MovementPattern.Flee:
                if (defectTimer > 0)
                {
                    if (Time.frameCount % 5 == 1)
                        defectTimer--;
                }
                else
                {
                    //if defect ocures, ships stops fleeing from player for certain time
                    if (Time.frameCount % 7 == 3 && defectChange != 0 && random.Next(0, 101) <= defectChange)
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            velocity.x = 0;
                        }
                        defectTimer = 10;
                    }
                    else
                    {
                        if (math.abs(transform.position.x - player.transform.position.x) > 1.5F)
                        {
                            velocity = AdvancedAi(velocity, MovementPattern.Random);
                        }
                        else
                        {
                            if(math.abs(transform.position.x) <= GlobalVariables.playAreaSize)
                            {
                                velocity.x = player.transform.position.x < transform.position.x ? horizontalSpeed : horizontalSpeed * -1;
                            }
                            else
                            {
                                velocity.x = player.transform.position.x < 0 ? horizontalSpeed : horizontalSpeed * -1;
                                defectTimer = 10;
                            }
                        }
                    }
                }
                if (math.abs(transform.position.x) >= GlobalVariables.playAreaSize)
                {
                    velocity.x = 0;
                }
                break;
        }
        return velocity;
    }
}
