using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{

    public float aspectRatio = 12.0f / 16.0f;

    // Start is called before the first frame update
    void Start()
    {
        Adjustment();

    }

    public void Adjustment()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / aspectRatio;
        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f) //maintains aspect ratios regardless of devices
        {

            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;

        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
