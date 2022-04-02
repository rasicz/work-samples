using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed;
    public float horizontalSpeed;
    public int damage;
    protected Renderer renderer;
    protected Rigidbody2D rigidbody;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(horizontalSpeed, speed);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!renderer.isVisible)
        {
            GameObject.Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        bool destroy = false;
        if(collider.GetComponent<Shield>() != null)
        {
            collider.GetComponent<Shield>().ReciveDamage(damage);
            destroy = true;
        }
        else if(collider.gameObject.layer == GlobalVariables.layers[1])
        {
            collider.GetComponent<HealthManager>().ReciveDamage(damage);
            destroy = true;
        } 
        else if (collider.gameObject.layer == GlobalVariables.layers[0])
        {
            collider.GetComponent<PlayerHealthManager>().ReciveDamage(damage);
            destroy = true;
        }
        if(destroy) Destroy(gameObject);
    }
}
