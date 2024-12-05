using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ManageScore : MonoBehaviour
{

    private TMP_Text scoreText;
    
    private int score = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.gameObject.GetComponent<TMP_Text>();

        scoreText.text = "SCORE : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddScore(4);
        }
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "SCORE: " + score.ToString();
        
    }

    public void RemoveScore(int removedScore)
    {
        if (score - removedScore < 0)
        {
            score = 0;
        } else
        {
            score -= removedScore;
        }
        scoreText.text = "SCORE: " + score.ToString();

    }

   

}
