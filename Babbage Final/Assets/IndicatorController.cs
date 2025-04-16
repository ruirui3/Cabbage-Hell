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
    

    //testing variables
    private LineRenderer lineRenderer;
    private float testTimer = 0f;
    private float testInterval = 2f; // Generate a new vector every 2 seconds

    void Start()
    {
        //random number testing
        // int randNum = Random.Range(-3,3);
        // Debug.Log($"random number: {randNum}");

            // Set up a LineRenderer to visualize the vector testing
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.yellow;

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

    private Vector2 GenerateRandomTopToBottomVector() {
        // get the camera to convert screen coordinates to world coordinates
        Camera cam = Camera.main;
        
        // Calculate world coordinates for top and bottom screen edges
        Vector2 topLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 bottomRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
        // Get random x positions at top and bottom of screen
        float topX = UnityEngine.Random.Range(topLeft.x, topRight.x);
        float bottomX = UnityEngine.Random.Range(bottomLeft.x, bottomRight.x);
        
        // Create start and end points
        Vector2 start = new Vector2(topX, topLeft.y);
        Vector2 end = new Vector2(bottomX, bottomLeft.y);
        
        // Calculate and return the direction vector
        return (end - start).normalized;
    }


    //vizualize and print vector dir to make sure randomization works
    private void GenerateAndVisualizeVector() {
    
    dir = GenerateRandomTopToBottomVector();
    
    // make linerenderer show path
    lineRenderer.SetPosition(0, start);
    lineRenderer.SetPosition(1, end);

    Debug.Log($"New Vector - Start: {start}, End: {end}, Direction: {dir}");
}
}
