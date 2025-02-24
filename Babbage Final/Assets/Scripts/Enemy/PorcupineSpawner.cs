using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineSpawner : MonoBehaviour
{
    public GameObject porcupe;
    Transform[] spawnPoints; 
    public float delayTime = 1f;
    public float randXRange = 3.2f;
    public float randX;
    public float setY = 4f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnPorc", 1f, delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnPorc() {
        float randX = Random.Range(-randXRange, randXRange);
        
        Instantiate(porcupe, new Vector2(randX, setY), transform.rotation);
    }
}
