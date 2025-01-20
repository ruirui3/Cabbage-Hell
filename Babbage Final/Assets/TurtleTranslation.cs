using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleTranslation : MonoBehaviour
{
    public GameObject moveTo;
    public float initSpeed = 2f;
    public float rotatingSpeed = .5f;
    public float bottomBound = -5f;
    public float rotationRate = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.down * Time.deltaTime * initSpeed);
        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
        
        if (Mathf.Abs(transform.position.y - moveTo.transform.position.y) < 2.5) {
            transform.position = Vector2.MoveTowards(transform.position, moveTo.transform.position, rotatingSpeed * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotationRate);
        }
        
    }
}
