using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public Sprite[] sprites;
    public Material[] materials;
    public int[] layers;
    public Sprite[] explosions;
    public GameObject[] pickUps;
    public GameObject[] playerShips;
    public GameObject deathScreen;
    void Update()
    {
        GlobalVariables.layers = layers;
    }
}
