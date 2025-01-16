using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 10; //can be adjusted for float
    public int currentHealth;
    public int regenerationCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        

    }

    // Update is called once per frame, NOTE TO TEAM - limit the FPS of game to adjust for Update(). Since FPS is uncapped, the game can function very fast on high end devices while slow on lower end devices. 
    void Update()
    {
        regenerationCount++;
        if (regenerationCount == 2000)
        {
            AddHealth(1);
            regenerationCount = 0;
            
        }

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= damage)
        {
            //dead or end screen or restart
            Debug.Log("dead");

        } else
        {
            currentHealth -= damage;
            Debug.Log(currentHealth.ToString());
        }

        
        
    }

    public void AddHealth(int health)
    {

        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += health;
        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int health)
    {
        if (maxHealth >= health)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth = health;
        }
    }




}
