using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrenade : Weapon
{
    protected int phase;
    public float preExplosionTime;
    // Start is called before the first frame update
    protected override void useWeapon()
    {
        foreach (Vector2 v in spawnLocations)
        {
            GameObject clone = GameObject.Instantiate(projectile);

            if (enemy) clone.layer = GlobalVariables.layers[3];

            clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
            clone.GetComponent<Grenade>().speed = projectileSpeed; 
            clone.GetComponent<Grenade>().damage = projectileDamage;
            clone.GetComponent<Grenade>().preExplosionTime = preExplosionTime;
        }
        phase++;
    }
}
