using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [HideInInspector]
    public int maxHealth;
    new void Start()
    {
        maxHealth = health;
        base.Start();

        //makes sure player ship is in next scene
        DontDestroyOnLoad(gameObject);
    }
    public new void ReciveDamage(int damage)
    {
        if (health <= 0) return;
        damage = 1;
        if (invulnerable) return;
        if (hitTime + 1 > Time.time) return;
        if (damage >= health)
        {
            health = 0;
            Destroy(true);
        }
        else
        {
            health -= damage;
            hitTime = Time.time;
        }
    }
    private GameObject deathScreen;
    public new void Destroy(bool effect)
    {
        gameObject.GetComponent<PlayerMovement>().MovementActive = false;
        gameObject.GetComponent<PlayerWeaponManager>().WeaponsActive = false;
        deathScreen = GameObject.Instantiate(GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>().deathScreen);
        animator.SetTrigger("destroy");
        StartCoroutine(EndScreen());
    }
    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(0.5F);
        for(int i = 0; i < 10; i++)
        {
            deathScreen.GetComponent<CanvasGroup>().alpha += 0.1F;
            yield return new WaitForSeconds(0.1F);
        }
        yield return new WaitForSeconds(0.1F);
        GameObject.Find("background end").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.FindGameObjectsWithTag("Enemy").ToList().Select(g => g.GetComponent<Rigidbody2D>().velocity = Vector2.zero);
        GlobalVariables.Paused = true;
        GameObject.Destroy(gameObject);
    }
}
