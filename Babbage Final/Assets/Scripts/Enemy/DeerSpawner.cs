using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSpawner : MonoBehaviour
{
    public GameObject porcupe;
    Transform[] spawnPoints;
    public float delayTime = 10f;
    public float randXRange = 3.2f;
    public float randX;
    public float setY = 4f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnDeer", 1f, delayTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnDeer()
    {
        float randX = Random.Range(-randXRange, randXRange);

        Instantiate(deer, new Vector2(randX, setY), transform.rotation);
    }
}
