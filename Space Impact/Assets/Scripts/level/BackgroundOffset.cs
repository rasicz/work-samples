using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    // Start is called before the first frame update
    float offset;
    void Start()
    {
        offset = GlobalVariables.backgroundOffset + -1F;
        transform.position += new Vector3(0, offset, 0);
    }
}
