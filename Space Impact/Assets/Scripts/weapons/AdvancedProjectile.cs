using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedProjectile : SimpleProjectile
{
    public bool wasVisible = false;
    new void Update()
    {
        if (!renderer.isVisible && wasVisible)
        {
            GameObject.Destroy(gameObject);
        } else if (renderer.isVisible && !wasVisible)
        {
            wasVisible = true;
        }
    }
}
