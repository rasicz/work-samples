using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : Weapon
{
    //public float laserDuration;
    public int onStayDamage;
    protected override void useWeapon()
    {
        foreach (Vector2 v in spawnLocations)
        {
            GameObject clone = GameObject.Instantiate(projectile);

            if (enemy) clone.layer = GlobalVariables.layers[3];

            clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
            clone.GetComponent<Laser>().damage = projectileDamage;
            clone.GetComponent<Laser>().onStayDamage = onStayDamage;

            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GlobalVariables.playerVerticalSpeed);
            clone.GetComponent<Laser>().parent = gameObject;
            clone.GetComponent<Laser>().difference = v;
        }
    }
}
