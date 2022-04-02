using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponRocketRain : Weapon
{
    // Start is called before the first frame update
    private int phase;
    private GameObject clone;
    public GameObject rainProjectile;
    private float timer;
    public float waitTime;
    public int numberOfRainProjectilesPerWave;
    public int numberOfWaves;
    public float timeBetweenWaves;
    private int wavePhase;
    public float startProjectileSpeed;
    public int numberOfStartProjectiles;
    private int StartProjectilesSpawned;
    System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
    protected override void Update()
    {
        if (Active)
        {
            switch (phase)
            {
                case 0:
                    if (exclusive && enemy)
                    {
                        gameObject.GetComponent<BossAi>().weaponsActive = false;
                        gameObject.GetComponent<BossAi>().weapons.ToList().ForEach(w => w.Active = false);
                    }
                    if(Time.frameCount % 2 == 0 && random.Next(0, 2) == 0)
                    {
                        clone = GameObject.Instantiate(projectile);
                        Vector2 v = new Vector2(GlobalVariables.RandomFloat(spawnLocations[0].x, spawnLocations[1].x), spawnLocations[0].y);
                        clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
                        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, startProjectileSpeed);
                        if (StartProjectilesSpawned < numberOfStartProjectiles) StartProjectilesSpawned++;
                        else phase++;
                    }
                    break;
                case 1:
                    if (clone.GetComponent<Renderer>().isVisible) break;
                    Destroy(clone);
                    timer = Time.time;
                    phase++;
                    break;
                case 2:
                    if (timer + waitTime > Time.time)
                    {
                        phase++;
                        timer = 0;
                    }
                    break;
                case 3:
                    if (Time.time >= timer + timeBetweenWaves)
                    {
                        for (int i = 0; i < numberOfRainProjectilesPerWave; i++)
                        {
                            GameObject g = GameObject.Instantiate(rainProjectile);
                            g.transform.position = new Vector2(random.Next(-56, 57) / 20F, Camera.main.transform.position.y + ( Camera.main.orthographicSize + 0.5F) * (enemy ? 1 : -1));
                            g.GetComponent<SpriteRenderer>().sprite = GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>().sprites[sprite];
                            g.GetComponent<AdvancedProjectile>().speed = projectileSpeed;
                            if (enemy) g.layer = GlobalVariables.layers[3];
                        }
                        timer = Time.time;
                        if (wavePhase >= numberOfWaves - 1) phase++;
                        else wavePhase++;
                    }
                    break;
                case 4:
                    if (Time.time >= timer + timeBetweenWaves)
                    {
                        Active = false;
                        if (exclusive)
                        {
                            gameObject.GetComponent<BossAi>().weaponsActive = true;
                            gameObject.GetComponent<BossAi>().weapons[0].Active = true;
                        }
                        phase = 0;
                    }
                    break;
            }
        }
    }
}
