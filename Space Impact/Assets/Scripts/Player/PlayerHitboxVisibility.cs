using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRight;
    Renderer renderer;
    public GameObject Player;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isVisible = !renderer.isVisible;
        if(isRight)
        {
            Player.GetComponent<PlayerMovement>().rightHitbox = isVisible;
        }
        else
        {
            Player.GetComponent<PlayerMovement>().leftHitbox = isVisible;
        }
    }
}
