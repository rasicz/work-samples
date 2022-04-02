using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public GameObject parent;
    public bool changeParent = false;
    public GameObject newParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ReciveDamage(int damage)
    {
        if(health != -1)
        {
            if (health == 0) return;
            if (damage >= health)
            {
                health = 0;
                parent.GetComponent<HealthManager>().invulnerable = false;
                if(changeParent)
                {
                    GameObject.Instantiate(newParent).transform.position = parent.transform.position;
                    GameObject.Destroy(parent);
                    GameObject.Destroy(gameObject);
                }
                Destroy(gameObject);
            }
            else
            {
                health -= damage;
            }
        }
    }
    void OnEnable()
    {
        parent.GetComponent<HealthManager>().invulnerable = true;
    }
    void OnDisable()
    {
        parent.GetComponent<HealthManager>().invulnerable = false;
    }
}
