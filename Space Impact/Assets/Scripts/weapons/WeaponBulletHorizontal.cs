using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponBulletHorizontal : Weapon
{
    public float[] ProjectileSpeedHorizontal;
    //public float[] angle;
    // Start is called before the first frame update
    protected override void useWeapon()
    {
        int i = 0;
        Sprite cloneSprite = GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>().sprites[sprite];
        foreach (Vector2 v in spawnLocations)
        {
            GameObject clone = GameObject.Instantiate(projectile);
            clone.GetComponent<SpriteRenderer>().sprite = cloneSprite;

            if (enemy) clone.layer = GlobalVariables.layers[3];

            clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
            clone.GetComponent<SimpleProjectile>().speed = projectileSpeed;
            clone.GetComponent<SimpleProjectile>().damage = projectileDamage;

            clone.GetComponent<SimpleProjectile>().horizontalSpeed = ProjectileSpeedHorizontal[i];
            float angle = 0;
            if (ProjectileSpeedHorizontal[i] != 0) angle = Mathf.Atan(ProjectileSpeedHorizontal[i] / projectileSpeed);
            angle = angle * (180 / Mathf.PI);
            clone.transform.rotation = Quaternion.Euler(0, 0, -angle);
            i++;
        }
    }

}
