using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class ShatteringGrenade : Grenade
{
    public int shatteringProjectileDamage;
    public float shatteringProjectileSpeed;
    public GameObject shatteringProjectile;
    public Vector2[] shatteringProjectileSpawnLocations;
    public float[] shatteringProjectileSpeedHorizontal;
    protected override void CustomUpdate()
    {
        switch (phase)
        {
            case 0:
                timer = Time.time;
                phase++;
                break;
            case 1:
                if (Time.time >= timer + preExplosionTime)
                {
                    animator.SetTrigger("explosion");
                    timer = Time.time;
                    phase++;
                }
                break;
            case 2:
                {
                    int i = 0;
                    foreach(Vector2 v in shatteringProjectileSpawnLocations)
                    {
                        GameObject g = GameObject.Instantiate(shatteringProjectile);
                        g.transform.position = new Vector2(transform.position.x, transform.position.y) + v;

                        g.GetComponent<SimpleProjectile>().speed = shatteringProjectileSpeed;
                        g.GetComponent<SimpleProjectile>().damage = shatteringProjectileDamage;

                        g.GetComponent<SimpleProjectile>().horizontalSpeed = shatteringProjectileSpeedHorizontal[i];
                        float angle = 0;
                        if (shatteringProjectileSpeedHorizontal[i] != 0) angle = Mathf.Atan(shatteringProjectileSpeedHorizontal[i] / shatteringProjectileSpeed);
                        angle = angle * (180 / Mathf.PI);
                        g.transform.rotation = Quaternion.Euler(0, 0, -angle);





                        i++;
                    }
                    phase++;
                }
                break;
            case 3:
                Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
                gameObject.GetComponent<BoxCollider2D>().size = S;
                break;
        }
    }
}
