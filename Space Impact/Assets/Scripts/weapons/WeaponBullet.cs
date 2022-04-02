using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : Weapon
{
    protected override void useWeapon()
    {
        foreach(Vector2 v in spawnLocations)
        {
            GameObject clone = GameObject.Instantiate(projectile);
            clone.GetComponent<SpriteRenderer>().sprite = GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>().sprites[sprite];

            if(enemy) clone.layer = GlobalVariables.layers[3];

            clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
            clone.GetComponent<SimpleProjectile>().speed = projectileSpeed;
            clone.GetComponent<SimpleProjectile>().damage = projectileDamage;
        }
    }
}
