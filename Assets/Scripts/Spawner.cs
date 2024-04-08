using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    public GameObject[] enemies;
    public float timeBetweenSpawns;
    public float holeSpawnTime;
    private float spawnTimer;
    private float timeRemaining = 2;
    private bool canSpawn;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = timeBetweenSpawns;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -5;
        
       
    }

    // Update is called once per frame
    void Update()
    {   
        holeSpawnTime -= Time.deltaTime;
        if (holeSpawnTime <= 0 )
        {
            spriteRenderer.sortingOrder = 0;
            canSpawn = true;
            timeRemaining -= Time.deltaTime;

            if (spawnTimer <= 0 && timeRemaining <= 0 && canSpawn)
            {
                int index = Random.Range(0, enemies.Length);
                GameObject enemy = enemies[index];
                Vector3 pos = this.gameObject.transform.position;
                // Quaternion for gimble lock prevention, spawn with Instantiate
                Instantiate(enemy, pos, Quaternion.identity);
                // reset timer
                spawnTimer = timeBetweenSpawns;
            
            }
            else if (timeRemaining <= 0)
            {   
                spawnTimer -= Time.deltaTime;

            }
        }
    }
}
