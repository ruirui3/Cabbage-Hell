using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootDropSequence : MonoBehaviour
{   

    private int state = 0;
    public Animator boxOpening;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case 0: // waiting for user to click, display text prompting click
                increaseStateOnClick();
                break;
            case 1: // user clicked, animating box opening and loot revealing
                boxOpening.Play("open_box");
                state++;
                break;
            case 2: // wait for loot to reveal, then waiting on user click to continue
                if (boxOpening.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) { // if animation finished playing
                    increaseStateOnClick();
                }
                break;
            case 3: // user interaction with box finished, clear everything off screen and "apply" loot
                break;
        }
    }

    void increaseStateOnClick() {
        if (Input.GetMouseButtonDown(0)) {
            state++;
        }
    }
}
