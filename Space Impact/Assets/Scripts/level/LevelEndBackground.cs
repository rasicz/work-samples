using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndBackground : MonoBehaviour
{
    Rigidbody2D rigidbody; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Active()
    {
        rigidbody.velocity = new Vector2(0 ,GlobalVariables.playerVerticalSpeed);
    }
}
