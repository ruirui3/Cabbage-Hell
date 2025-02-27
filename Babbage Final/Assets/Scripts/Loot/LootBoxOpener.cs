using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootBoxOpener : MonoBehaviour
{
    public Animator boxOpening;
    public LootUIManager lootUI;
    public Loot[] lootItems; // Assign loot items in Inspector
    public int state = 0;
    public Loot selectedLoot;

    void Update()
    {
        switch (state)
        {
            case 0: // Waiting for user click
                if (Input.GetKeyDown(KeyCode.Space))  //Input.GetMouseButtonDown(0), temp space
                {
                    OpenBox();
                }
                break;

            case 1: // Wait for animation to finish
                if (IsAnimationFinished("open_box"))
                {
                    RevealLoot();
                }
                break;

            case 2: // Loot shown, wait for user to acknowledge
                if (Input.GetKeyDown(KeyCode.Space))  //Input.GetMouseButtonDown(0), temp space
                {
                    ApplyLootAndClose();
                }
                break;
        }
    }

    void OpenBox()
    {
        boxOpening.Play("open_box");
        state = 1;
    }

    void RevealLoot()
    {
        selectedLoot = GetRandomLoot(); // Random loot
        Debug.Log("got loot");
        // Time.timeScale = 0;
        lootUI.ShowLoot(selectedLoot);
        state = 2;
    }

    void ApplyLootAndClose()
    {
        lootUI.HideLoot();
        state = 0;
    }

    bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo animState = boxOpening.GetCurrentAnimatorStateInfo(0);
        return animState.IsName(animationName) && animState.normalizedTime >= 1.0f;
    }

    Loot GetRandomLoot()
    {
        int totalWeight = 0;
        foreach (Loot item in lootItems)
        {
            totalWeight += item.dropChance;
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (Loot item in lootItems)
        {
            cumulativeWeight += item.dropChance;
            if (randomValue < cumulativeWeight)
            {
                return item;
            }
        }
        
        return lootItems[0]; // Fallback (should never be reached)
    }
}
