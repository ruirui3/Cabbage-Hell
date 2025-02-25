using UnityEngine;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // Assign in Inspector

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i, 0);
            scoreTexts[i].text = score;
        }
    }
}
