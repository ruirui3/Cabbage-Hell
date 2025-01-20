using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpawnerScript : MonoBehaviour
{
    public GameObject turtle;
    Transform[] spawnPoints; 
    public float delayTime = 1f;
    public float randXRange = 3.2f;
    public float randX;
    public float setY = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnTurtle", 1f, delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnTurtle() {
        float randX = Random.Range(-randXRange, randXRange);
        
        Instantiate(turtle, new Vector2(randX, setY), transform.rotation);
    }
}
