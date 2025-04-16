using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    //set variables
    private Vector2 start;
    private Vector2 end;
    private Vector2 dir;
    private float displayTime;
    private float xmin = -3f;
    private float xmax = 3f;
    private float ymin = -4.85f;
    private float ymax = 4.85f;
    

    //testing variables
    private LineRenderer lineRenderer;
    private float testTimer = 0f;
    private float testInterval = 2f; // Generate a new vector every 2 seconds

    void Start()
    {
        dir = GenerateRandVector();
        //random number testing
        // int randNum = Random.Range(-3,3);
        // Debug.Log($"random number: {randNum}");

            // Set up a LineRenderer to visualize the vector testing
            // lineRenderer = gameObject.AddComponent<LineRenderer>();
            // lineRenderer.startWidth = 0.1f;
            // lineRenderer.endWidth = 0.1f;
            // lineRenderer.positionCount = 2;
            // lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            // lineRenderer.startColor = Color.red;
            // lineRenderer.endColor = Color.yellow;

            //set active to see sprite?

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Generate a new vector every few seconds to test
        testTimer += Time.deltaTime;
        if (testTimer >= testInterval)
        {
            GenerateAndVisualizeVector();
            testTimer = 0f;
        }
    }

    

    private Vector2 GenerateRandVector() {
        // get the camera to convert screen coordinates to world coordinates
        Camera cam = Camera.main;
        
        //random horizontal spawn range
        float randomX = Random.Range(xmin, xmax);
    
        //Fixed y range -> minx and miny are already defined
        //return vectors
        Vector2 startPoint = new Vector2(randomX, ymax);
        Vector2 endPoint = new Vector2(randomX, ymin);
        //set start point to start and end to end
        start = startPoint;
        end = endPoint;
        
        // Calculate and return the direction vector
        return (endPoint - startPoint);
    }

    private void AlignIndicator(){
        // Position the indicator at the start point
        transform.position = start;
        
        // calculate angle to rotate based on the direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Scale the indicator to match the path length
        float pathLength = Vector2.Distance(start, end);
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(currentScale.x, pathLength, currentScale.z);
    }

    //vizualize and print vector dir to make sure randomization works
    private void GenerateAndVisualizeVector() {
    
    dir = GenerateRandVector();
    
    // make linerenderer show path
    lineRenderer.SetPosition(0, start);
    lineRenderer.SetPosition(1, end);

    Debug.Log($"New Vector - Start: {start}, End: {end}, Direction: {dir}");
}
}
