using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    Transform[] spawnPoints; 
    public int spawnCount = 0;
    float spawnInterval = 3f;
    float randomX;
    float fixedY = 1f;
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3f, spawnInterval); //cmd, offset, delay
        // implement if touch player, Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() //translation
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    void SpawnEnemy()
    {
        randomX = Random.Range(4, 6);
        Instantiate(Enemy, transform.position, transform.rotation); //obj, pos (use randomPos), rotation
        
    }
    
    

    
}
