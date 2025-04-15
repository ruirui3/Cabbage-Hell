using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallelLineIndicator : MonoBehaviour
{
    //make it so that you can see the variables in the editor also/
    [SerializeField] private GameObject targetSprite; // The sprite that will follow the path
    [SerializeField] private float displayDuration = 2.0f; // How long the indicator stays visible
    [SerializeField] private float targetMovementSpeed = 5.0f; // Speed of the target sprite
    [SerializeField] private bool autoStart = false; // Whether to start automatically
    
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private bool isActive = false;
    
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        // Hide the indicator at the start
        SetIndicatorVisible(false);
        
        if (autoStart)
        {
            StartCoroutine(RunSequence());
        }
    }
    
    public void StartSequence()
    {
        if (!isActive)
        {
            StartCoroutine(RunSequence());
        }
    }
    
    private IEnumerator RunSequence()
    {
        isActive = true;
        
        // Generate random path
        GenerateRandPath();
        
        // Align and show the indicator
        AlignIndicator();
        SetIndicatorVisible(true);
        
        // Wait for display duration
        yield return new WaitForSeconds(displayDuration);
        
        // Hide the indicator
        SetIndicatorVisible(false);
        
        // Make the target sprite follow the path
        yield return StartCoroutine(MoveTargetSprite());
        
        isActive = false;
    }
    
    private void GenerateRandPath()
    {
        // Convert screen edges to world coordinates
        Vector2 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
        // Using fully qualified name for Random.Range
        float randTopX = GetRandomRange(topLeft.x, topRight.x);
        startPoint = new Vector2(randTopX, topLeft.y);
        
        float randBottomX = GetRandomRange(bottomLeft.x, bottomRight.x);
        endPoint = new Vector2(randBottomX, bottomLeft.y);
        
        // Calculate direction vector
        direction = (endPoint - startPoint).normalized;
    }
    
    // Helper method to avoid Random class issues
    private float GetRandomRange(float min, float max)
    {
        // Using System.Random as a fallback if UnityEngine.Random causes issues
        System.Random systemRand = new System.Random();
        return (float)(systemRand.NextDouble() * (max - min) + min);
    }
    
    private void AlignIndicator()
    {
        // Position the indicator at the start point
        transform.position = startPoint;
        
        // Calculate the angle to rotate based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Adjust the angle based on sprite's default orientation
        angle -= 90;
        
        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Scale the indicator to match the path length
        float pathLength = Vector2.Distance(startPoint, endPoint);
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(currentScale.x, pathLength, currentScale.z);
    }
    
    private void SetIndicatorVisible(bool visible)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.enabled = visible;
        }
    }
    
    private IEnumerator MoveTargetSprite()
    {
        if (targetSprite == null)
            yield break;
        
        // Position target at start
        targetSprite.transform.position = startPoint;
        targetSprite.SetActive(true);
        
        // Rotate target to face direction of movement
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetSprite.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust based on sprite orientation
        
        float journeyLength = Vector2.Distance(startPoint, endPoint);
        float startTime = Time.time;
        
        // Move until reaching the end point
        while (Vector2.Distance(targetSprite.transform.position, endPoint) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * targetMovementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            
            targetSprite.transform.position = Vector2.Lerp(startPoint, endPoint, fractionOfJourney);
            
            yield return null;
        }
        
        // Ensure target reaches exactly the end point
        targetSprite.transform.position = endPoint;
    }
    
    // Helper method to visualize the path in the editor
    private void OnDrawGizmos()
    {
        if (isActive)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}