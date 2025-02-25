using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBullet : MonoBehaviour
{

    public bool isMoving = true;
    public float speed = 2f;
    public GameObject tornado;


    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");

        //Can also include scripts and other canvas variables



    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (notInScreen())
        {
            isMoving = false;
            gopoof();
        }
    }

    private bool notInScreen()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y > 1; //1 is top of the screen
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        gopoof();
    }
    private void gopoof()
    {
        // Disable the GameObject - gives an error when playercontroller code tries to respawn it? 
        //gameObject.SetActive(false);


        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            //instantiate tornado, delete itself

            Instantiate(tornado, transform);


        }
    }

}
