using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUISwitch : MonoBehaviour
{
    public PlayerController playerController;

    public Image currentImage;
    public Image nextImage;
    public Sprite basic;
    public Sprite carrot;
    public Sprite curling;
    public Sprite honey;
    public Sprite noBullet;
    public Sprite tornado;
    public Sprite triple;
    public int currentBulletType;
    public int nextBulletType;

    // Start is called before the first frame update
    void Start()
    {
        //Image 1 is Basic, image 2 is N/A
        currentImage.sprite = basic;
        nextImage.sprite = noBullet;

    }

    // Update is called once per frame
    void Update()
    {
        currentBulletType = playerController.PeekFirstBullet();
        nextBulletType = playerController.PeekSecondBullet();

        ChangeCurrentImage(currentBulletType);
        ChangeNextImage(nextBulletType);

    }

    public void ChangeCurrentImage(int currentBulletIndex)
    {
        currentImage.sprite = GetBulletSprite(currentBulletIndex);
    }
    public void ChangeNextImage(int currentBulletIndex)
    {
    nextImage.sprite = GetBulletSprite(currentBulletIndex);
}

    private Sprite GetBulletSprite(int bulletIndex)
    {
        switch (bulletIndex)
        {
            case 0: return basic;
            case 1: return triple;
            case 2: return carrot;
            case 3: return honey;
            case 4: return curling;
            case 5: return tornado;
            default: return noBullet;
        }
    }

}
