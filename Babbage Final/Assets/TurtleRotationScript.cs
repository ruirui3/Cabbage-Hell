using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurtleRotationScript : MonoBehaviour
{
    public float rotationRate = 2f; 
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRotating", false);
    }

    // Update is called once per frame
    void Update()
    {   
        if (gameObject.GetComponentInParent<TurtleTranslation>().isRotating) {
            animator.SetBool("isRotating", true);
            transform.Rotate(Vector3.forward * rotationRate);
        }
    }
}
