using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : SimpleProjectile
{
    //public float speed;
    //public float horizontalSpeed;
    //public int damage;
    public GameObject parent;
    //protected Renderer renderer;
    //protected Rigidbody2D rigidbody;
    protected Collider2D collider;
    // Start is called before the first frame update
    public Vector2 difference;
    public int onStayDamage;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(parent == null)
        {
            Destroy();
        }
        else
        {
            transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y) + difference;
        }
    }
    public void StartDamage()
    {
        collider.enabled = true;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    new protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Shield>() != null)
        {
            collider.GetComponent<Shield>().ReciveDamage(damage);
            return;
        }
        if (collider.gameObject.layer == GlobalVariables.layers[1])
        {
            collider.GetComponent<HealthManager>().ReciveDamage(damage);
        }
        if (collider.gameObject.layer == GlobalVariables.layers[0])
        {
            collider.GetComponent<PlayerHealthManager>().ReciveDamage(damage);
        }
    }
    protected void OnTriggerStay2D(Collider2D collider)
    {
        if (Time.frameCount % 15 != 0) return;

        if (collider.GetComponent<Shield>() != null)
        {
            collider.GetComponent<Shield>().ReciveDamage(damage);
            return;
        }
        if (collider.gameObject.layer == GlobalVariables.layers[1])
        {
            collider.GetComponent<HealthManager>().ReciveDamage(damage);
        }
        if (collider.gameObject.layer == GlobalVariables.layers[0])
        {
            collider.GetComponent<PlayerHealthManager>().ReciveDamage(damage);
        }
    }
}
