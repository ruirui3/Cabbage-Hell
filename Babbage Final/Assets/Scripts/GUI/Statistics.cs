using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * For future, statistic values would be called to enhance or modify the performance of behaviors in their own respective fields. 
 */
public class Statistics : MonoBehaviour
{
    private int vitality;
    private int intelligence;
    private int reju;
    private int agility;
    private int currency;

    //other variables can include the calculation for health, mana, rejuvanating speed, how fast the player goes (limited?) for references

    // Start is called before the first frame update
    void Start()
    {
        vitality = 0;
        intelligence = 0;
        reju = 0;
        agility = 0;
        currency = 0;
    }

    void SetStats(int vit, int intel, int rej, int agi, int cur)
    {
        vitality = vit;
        intelligence = intel;
        reju = rej; 
        agility = agi;  
        currency = cur;

    }

    void SetCurrency(int cur)
    {
        currency = cur;
    }

    void AddCurrency(int cur)
    {
        currency += cur;
    }

    void RemoveCurrency(int cur)
    {
        if (currency - cur >= 0)
        {
            currency -= cur;
        }
        else
        {
            Debug.Log("Error: current balance is less than the intended price of item");
        }


        
    }

    void AddVitality()
    {
        vitality++;
    }

    void AddIntelligence()
    {
        intelligence++;
    }

    void AddReju()
    {
        reju++;
    }

    void AddAgility()
    {
        agility++;
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
