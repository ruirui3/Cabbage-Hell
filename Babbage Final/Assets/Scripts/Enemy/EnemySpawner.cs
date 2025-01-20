using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    Transform[] spawnPoints; 
    public int spawnCount = 0;
    float spawnInterval = 0.5f;
    float randomX;
    private float fixedY = 5f;
    private float randomXMin = -3f;
    private float randomXMax = 3f;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval); //cmd, offset, delay
        // implement if touch player, Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() //translation
    {
        
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(randomXMin, randomXMax);
        
        Instantiate(Enemy, new Vector2(randomX, fixedY), transform.rotation); //obj, pos (use randomPos), rotation
        
    }
    
    

    
}
