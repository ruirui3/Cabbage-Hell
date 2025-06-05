using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeloAccelManager : MonoBehaviour
{
    public Image velocityBar;

    private float maxVelocity;
    private float currentVelocity;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
        maxVelocity = playerController.GetMaxVelocity();
        currentVelocity = playerController.GetVelocity();
        velocityBar.fillAmount = (currentVelocity / maxVelocity);   //0 is 0%, 1 is 100% 

    }

    // Update is called once per frame
    void Update()
    {
        maxVelocity = playerController.GetMaxVelocity();
        currentVelocity = playerController.GetVelocity();
        velocityBar.fillAmount = (currentVelocity / maxVelocity);   //0 is 0%, 1 is 100% 



    }
}
