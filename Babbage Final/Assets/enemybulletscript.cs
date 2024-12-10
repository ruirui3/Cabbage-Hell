using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybulletscript : MonoBehaviour
{
    public float speed = 5f;
    public bool isMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){ 
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        if (notInScreen()){
            isMoving = false;
            gopoof();
        }

    }
    private bool notInScreen(){
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y < 0; //0 is bottom of the screen
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        gopoof();
    }
    private void gopoof() {
        // Disable the GameObject - gives an error when playercontroller code tries to respawn it? 
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "cabbage cart") {
            gopoof();
        }
    }
}
