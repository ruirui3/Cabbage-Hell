using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotationScript : MonoBehaviour
{
    public float rotationRate = 2f; 
    public PlayerController playerController;

    private int currentBulletType;
    // Start is called before the first frame update
    void Start()
    {

        // if (currentBulletType == 2) {//carrot
        //     GetComponent<SpriteRenderer>().enabled = false;
        // }
        // transform.Rotate(Vector3.forward * rotationRate * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward * rotationRate * Time.deltaTime);
        transform.Rotate(0, 0, 180 * Time.deltaTime);
        //currentBulletType = playerController.GetBulletType();
        
        
    }
}
