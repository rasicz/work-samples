using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFrequency : MonoBehaviour
{
    public int updateFrequency;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = updateFrequency;
        Debug.Log("update frequency: " + updateFrequency);
    } 
}
