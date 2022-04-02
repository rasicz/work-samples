using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : SimpleProjectile
{
    // Start is called before the first frame update
    protected GameObject target;
    protected System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, speed);
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && Time.frameCount % 4 == 0)
        {
            if(Camera.main.transform.position.y > transform.position.y)
            FindTarget();
        }

        if (!renderer.isVisible)
        {
            GameObject.Destroy(gameObject);
        }
        Movement();
    }
    void FindTarget()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> viableObjects = new List<GameObject>();
        bool viableTarget = false;
        foreach(GameObject g in gameObjects)
        {
            if (g.GetComponent<Renderer>().isVisible)
            {
                viableObjects.Add(g);
                viableTarget = true;
            }
        }
        if (!viableTarget) return;
        int randomInt = random.Next(0, viableObjects.Count);
        target = viableObjects[randomInt];
    }
    void Movement()
    {
        if(target == null)
        {
            
        }
        else
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed / 10);
            //GlobalVariables.toVector2(target.transform.position));
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            rigidbody.velocity = Vector2.zero;
            rigidbody.AddRelativeForce(new Vector2(0, speed), ForceMode2D.Impulse);
        }
    }
}
