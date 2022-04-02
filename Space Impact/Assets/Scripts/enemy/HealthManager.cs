using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    protected float hitTime;
    protected Material halfOpacity;
    protected Material fullOpacity;
    protected SpriteRenderer spriteRenderer;
    protected Sprite[] explosions;
    protected Animator animator;
    [Tooltip("0-100 %")]
    public float dropChance;
    private SpriteHolder spriteHolder;
    private PickUpsManager pickUpsManager;
    private GameObject[] pickUps;
    bool normalMaterial = true;
    protected System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());
    public float destroyTime = 0.15F;
    public bool invulnerable;

    protected void Start()
    {
        spriteHolder = GameObject.Find("ScriptHolder").GetComponent<SpriteHolder>();
        pickUpsManager = GameObject.Find("ScriptHolder").GetComponent<PickUpsManager>();
        halfOpacity = spriteHolder.materials[0];
        fullOpacity = spriteHolder.materials[1];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        explosions = spriteHolder.sprites;
        animator = gameObject.GetComponent<Animator>();
        pickUps = spriteHolder.pickUps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if( Time.frameCount % 6 == 0)
        {
            if (hitTime != 0 && hitTime + 1 > Time.time) 
            { Flashing(); }
            else if (hitTime != 0 && hitTime + 1 < Time.time) 
            { 
                hitTime = 0;
                spriteRenderer.material = fullOpacity;
                normalMaterial = true;
            }
;        }
    }

    public void ReciveDamage(int damage)
    {
        if (invulnerable) return;
        if (health == 0) return;
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
    public void Destroy(bool effect)
    {
        animator.SetTrigger("destroy");
        Destroy(gameObject, destroyTime);
        if(dropChance != 0)
        {
            if(dropChance * GlobalVariables.dropChance >= random.Next(0, 101))
            {
                GameObject.Instantiate(pickUps[pickUpsManager.RandomPickUp()]).transform.position = transform.position;
            }
            
        }
    }
    protected void Flashing()
    {
        if (normalMaterial) spriteRenderer.material = halfOpacity; 
        else spriteRenderer.material = fullOpacity;
        normalMaterial = !normalMaterial;
    }
}
