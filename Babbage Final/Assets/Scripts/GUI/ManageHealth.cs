using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageHealth : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthAmount;
    }

    // Update is called once per frame
    void Update()
    {
        

        

        //test cases 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }


    }

    public void TakeDamage(float damage)
    {

        if (healthAmount-damage <=0)
        {
            healthAmount = 0; //maybe trigger game end
        } else
        {
            healthAmount -= damage;
        }

        
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float heal)
    {

        if (healthAmount+heal > maxHealth)
        {
            healthAmount = maxHealth;
        } else
        {
            healthAmount += heal;
        }

        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

}
