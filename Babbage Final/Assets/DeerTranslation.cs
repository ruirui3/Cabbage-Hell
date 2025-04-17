using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerTranslation : MonoBehaviour
{
    float speed = 2.5f;
    public float bottomBound = -5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y < bottomBound){
            goPoof();
        }
    }
    void goPoof() {
        Destroy(gameObject);
    }
}
