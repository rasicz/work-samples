using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsManager : MonoBehaviour
{
    System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
    // Start is called before the first frame update
    [Tooltip("0-10")]
    public int[] veight;

    public int RandomPickUp()
    {
        int total = 0;
        foreach(int i in veight)
        {
            total += i;
        }
        int value = random.Next(0, total);
        total = 0;
        for(int i = 0; i < veight.Length; i++)
        {
            total += veight[i];
            if(total > value)
            {
                value = i;
                i = veight.Length;
            }
        }
        return value;
    }
}
