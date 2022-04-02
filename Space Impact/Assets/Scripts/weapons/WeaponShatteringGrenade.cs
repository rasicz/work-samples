using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShatteringGrenade : WeaponGrenade
{
    // Start is called before the first frame update
    public int secondProjectileDamage;
    public GameObject secondProjectile;
    public float secondProjectileSpeed;
    public Vector2[] secondProjectileSpawnLocations;
    public float[] secondProjectileSpeedHorizontal;
    protected override void useWeapon()
    {
        foreach (Vector2 v in spawnLocations)
        {
            GameObject clone = GameObject.Instantiate(projectile);

            if (enemy) clone.layer = GlobalVariables.layers[3];

            clone.transform.position = new Vector2(transform.position.x, transform.position.y) + v;
            clone.GetComponent<ShatteringGrenade>().speed = projectileSpeed; //pak to nezapomeň změnit! //změnit co?
            clone.GetComponent<ShatteringGrenade>().damage = projectileDamage;
            clone.GetComponent<ShatteringGrenade>().preExplosionTime = preExplosionTime;
            clone.GetComponent<ShatteringGrenade>().damage = secondProjectileDamage;
            clone.GetComponent<ShatteringGrenade>().shatteringProjectile = secondProjectile;
            clone.GetComponent<ShatteringGrenade>().shatteringProjectileSpawnLocations = secondProjectileSpawnLocations;
            clone.GetComponent<ShatteringGrenade>().shatteringProjectileSpeed = secondProjectileSpeed;
            clone.GetComponent<ShatteringGrenade>().shatteringProjectileSpeedHorizontal = secondProjectileSpeedHorizontal;
        }
        phase++;
    }
}
