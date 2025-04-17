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
    public int deerDelayTime = 4;
    public int indicatorDelayTime = 3;
    public int porcupineDelayTime = 8;
    public float randXRange = 3.2f;
    public float randX;
    public float setY = 4.85f;
    private float msDeer = 0f;
    private float msTurtle = 0f;
    private float msRabbit = 0f;
    private float msPorcupine = 0f;
    private float msInd = 0f;

    private Vector2 start;
    private Vector2 end;
    private Vector2 direction;

    private float xmin = -2f;
    private float xmax = 2f;
    //private float ymin = -4.85f;
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
        msDeer += Time.deltaTime;
        msInd += Time.deltaTime;

        SpawnTurtle();
        SpawnRabbit();
        SpawnPorcupine();
        SpawnDeer();
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


    public void SpawnDeer()
    {
        if (msInd >= indicatorDelayTime) {
            // direction = GenerateRandVector();
            // RotateAmt();
            //instantiate indicator
            float randX = Random.Range(xmin,xmax);
            Instantiate(indicator, new Vector2(randX, 0), transform.rotation);
            //get deer rotate same directoin as indicator
            msInd = 0;
             
        }
        if (msDeer >= deerDelayTime){
            //Instantiate(deer, start, transform.rotation);
            Instantiate(deer, new Vector2(randX, setY), transform.rotation);

            msDeer = 0;
        }

    }
    // private Vector2 GenerateRandVector() {
        
    //     //random horizontal spawn range
    //     float randomX = Random.Range(xmin, xmax);
    
    //     //Fixed y range -> minx and miny are already defined
    //     //return vectors
    //     Vector2 startPoint = new Vector2(randomX, setY);
    //     Vector2 endPoint = new Vector2(randomX, -setY);
    //     //set start point to start and end to end
    //     start = startPoint;
    //     end = endPoint;
    //     // calculate angle to rotate based on the direction
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     // Apply rotation
    //     transform.rotation = Quaternion.Euler(0, 0, angle);
        
    //     // Calculate and return the direction vector
       
    //     return (endPoint - startPoint);
    // }


}
