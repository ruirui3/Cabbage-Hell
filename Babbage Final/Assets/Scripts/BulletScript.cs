using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (notInScreen()){
            isMoving = false;
            gopoof();
        }

    }
    private bool notInScreen(){
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y > 1; //1 is top of the screen
    }
    private void whenCollide(Collision2D collision) {
        gopoof();
    }
    private void gopoof() {
        // Disable the GameObject - gives an error when playercontroller code tries to respawn it? 
        //gameObject.SetActive(false);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Hide the bullet
        }
    }
}
