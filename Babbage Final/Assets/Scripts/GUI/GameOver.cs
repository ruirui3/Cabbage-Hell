using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject img;
    public ManageHealth player;
    public ManageScore manageScore;

    private bool gameEnded = false;

    void Start()
    {
        img.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded && player.healthAmount <= 0)
        {
            gameEnded = true;
            img.SetActive(true);
            ScoreManager.Instance.SaveScore(manageScore.GetScore());
        }
    }
}
