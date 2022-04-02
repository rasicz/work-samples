using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

public class Grenade : SimpleProjectile
{
    protected int phase;
    public float preExplosionTime;
    public Animator animator;
    protected float timer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        CustomUpdate();
        base.Update();
    }
    protected virtual void CustomUpdate()
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
                    Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
                    gameObject.GetComponent<BoxCollider2D>().size = S;
                }
                break;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        bool explode = false;
        if (collider.GetComponent<Shield>() != null)
        {
            collider.GetComponent<Shield>().ReciveDamage(damage);
            explode = true;
        }
        else if (collider.gameObject.layer == GlobalVariables.layers[1])
        {
            collider.GetComponent<HealthManager>().ReciveDamage(damage);
            explode = true;
        }
        else if (collider.gameObject.layer == GlobalVariables.layers[0])
        {
            collider.GetComponent<PlayerHealthManager>().ReciveDamage(damage);
            explode = true;
        }
        if (explode && phase < 2)
        {
            timer -= preExplosionTime;
            phase = 1;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
