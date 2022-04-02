using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFTrigger : MonoBehaviour
{
    public GameObject bfBackground;
    private Renderer renderer;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
        {
            bfBackground.GetComponent<LevelEndBackground>().Active();
            Destroy(this);
        }
    }
}
