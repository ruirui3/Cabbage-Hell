using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotationScript : MonoBehaviour
{
    public float rotationRate = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        // transform.Rotate(Vector3.forward * rotationRate * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationRate * Time.deltaTime);
    }
}
