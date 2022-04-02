using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnType { Random, Defined }
    public SpawnType spawnType;
    public GameObject spawnedObject;
    public int numberOfSpawn;
    public float spawnDistance = 10;

    [Header("Random")]
    public float maxHorizontalDistance;
    public float maxVerticalDistance;
    System.Random random;

    [Header("Defined")]
    public Vector2[] positions;
    public float[] timesBeforeStaing;

    [Header("")]
    public float[] StayOnSceneTimes;

    [Header("Difficulty")]
    public bool easy = true;
    public bool medium = true;
    public bool hard = true;

    void Start()
    {
        random = new System.Random(System.Guid.NewGuid().GetHashCode());
        int difficulty = GlobalVariables.GameDifficulty;
        if (difficulty == 0 && !easy || difficulty == 1 && !medium || difficulty == 2 && !hard) Destroy(gameObject);
    }

    void Update()
    {
        //activates when near main camera
        if(Camera.main.transform.position.y + Camera.main.orthographicSize + spawnDistance >= transform.position.y)
        {
            Active();
        }
    }
    void Active()
    {              
         Spawn();     
         Destroy(gameObject);           
    }
    void Spawn()
    {
        for (int i = 0; i < numberOfSpawn; i++) //random.Next(min, max);
        {
            GameObject spawnedEntity = GameObject.Instantiate(spawnedObject);
            if (spawnType == SpawnType.Defined)
            {
                spawnedEntity.transform.position = GlobalVariables.toVector2(transform.position) + positions[i];
                if(timesBeforeStaing.Length > i) spawnedEntity.GetComponent<EnemyAi>().TimeBeforeStaying = timesBeforeStaing[i];
            }
            if (spawnType == SpawnType.Random)
            {
                float positionX = random.Next((int)(maxHorizontalDistance * -10), (int)(maxHorizontalDistance * 10 + 1));
                positionX /= 10;
                float positionY = random.Next((int)(maxVerticalDistance * -10), (int)(maxVerticalDistance * 10 + 1));
                positionY /= 10;
                spawnedEntity.transform.position = new Vector2(transform.position.x + positionX, transform.position.y + positionY);
            }
            if(StayOnSceneTimes.Length > i) spawnedEntity.GetComponent<EnemyAi>().StayOnSceneTime = StayOnSceneTimes[i];
        }
    }
}
