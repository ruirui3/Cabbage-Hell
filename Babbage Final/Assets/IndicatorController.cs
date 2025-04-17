using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{

    void Start()
    {
        Invoke("DestroyObject", 1f); // Call DestroyObject function after 1 second

    }

    void DestroyObject()
    {
        Destroy(gameObject); // Destroy the GameObject
    }

    // Update is called once per frame
    void Update()
    {

    }
}
