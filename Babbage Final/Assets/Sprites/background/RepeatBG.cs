using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    Vector3 startPos;
    float repeatHeight;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // remember starting pos as current position
        repeatHeight = gameObject.GetComponent<Renderer>().bounds.size.y; // repeat for half of image height
    }

    // Update is called once per frame
    void Update()
    {
        // if out of screen, go back to start pos
        if (transform.position.y < startPos.y - repeatHeight) {
            transform.position = startPos;
        }
    }
}
