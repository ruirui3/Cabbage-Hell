using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class ScoreboardManager : MonoBehaviour
{
    private TMP_Text[] scoreTexts;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scoreboard")
        {
            // Get all tagged ScoreText objects and order them by name
            GameObject[] textObjects = GameObject.FindGameObjectsWithTag("ScoreText");

            scoreTexts = textObjects
                .Select(obj => obj.GetComponent<TMP_Text>())
                .OrderBy(t => t.name) // Assuming ScoreText1, ScoreText2, etc.
                .ToArray();

            DisplayHighScores();
        }
    }

    public void DisplayHighScores()
    {
        List<int> scores = ScoreManager.Instance.GetHighScores();

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < scores.Count)
                scoreTexts[i].text = scores[i].ToString("D8");
            else
                scoreTexts[i].text = ""; // Leave empty if no score yet
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ScoreManager.Instance.ClearAllScores();
            DisplayHighScores(); // Refresh the display immediately
        }
    }

}
