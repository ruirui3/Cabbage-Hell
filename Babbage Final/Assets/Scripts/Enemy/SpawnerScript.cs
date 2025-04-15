using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject turtle;
    public GameObject rabbit;
    public GameObject porcupine;
    public GameObject indicator;
    public GameObject deer;
    public int turtleDelayTime = 3;
    public double rabbitDelayTime = 1.5;
    public int porcupineDelayTime = 8;
    public float randXRange = 3.2f;
    public float randX;
    public float setY = 4.85f;
    private float msTurtle = 0f;
    private float msRabbit = 0f;
    private float msPorcupine = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        msTurtle += Time.deltaTime;
        msRabbit += Time.deltaTime;
        msPorcupine += Time.deltaTime;

        SpawnTurtle();
        SpawnRabbit();
        SpawnPorcupine();
    }

    public void SpawnTurtle()
    {
        if (msTurtle >= turtleDelayTime)
        {
            float randX = Random.Range(-randXRange, randXRange);
            Instantiate(turtle, new Vector2(randX, setY), transform.rotation); //change turtle to enemy type
            msTurtle = 0;
        }
    }

    public void SpawnRabbit()
    {
        if (msRabbit >= rabbitDelayTime)
        {
            float randX = Random.Range(-randXRange, randXRange);
            Instantiate(rabbit, new Vector2(randX, setY), transform.rotation); //change turtle to enemy type
            msRabbit = 0;
        }
    }

    public void SpawnPorcupine()
    {
        if (msPorcupine >= porcupineDelayTime)
        {
            float randX = Random.Range(-randXRange, randXRange);
            Instantiate(porcupine, new Vector2(randX, setY), transform.rotation); //change turtle to enemy type
            msPorcupine = 0;
        }
    }

}
