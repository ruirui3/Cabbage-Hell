using System.Collections;
using UnityEngine;
using TMPro;

public class ManageScore : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;

    private int score = 0;
    private int combo = 0;
    private Vector3 comboOriginalScale;

    public int GetScore() => score;

    void Start()
    {
        scoreText.text = score.ToString("D8");
        comboText.text = "x" + combo.ToString();
        comboOriginalScale = comboText.rectTransform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCombo(1); // Simulate combo
            AddScore(4); // Simulate hit
        }

        if (Input.GetKeyDown(KeyCode.M)) // Simulate miss
        {
            ResetCombo();
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Simulate game end
        {
            ScoreManager.Instance.SaveScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scoreboard");
        }
    }

    public void AddScore(int addedScore)
    {
        float multiplier = combo < 2 ? 1f : combo / 2f;
        score += Mathf.RoundToInt(addedScore * multiplier);
        UpdateUI();
    }

    public void RemoveScore(int removedScore)
    {
        score = Mathf.Max(0, score - removedScore);
        UpdateUI();
    }

    public void AddCombo(int addedCombo)
    {
        combo += addedCombo;
        UpdateUI();
        StartCoroutine(AnimateComboPop());
    }

    public void ResetCombo()
    {
        combo = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString("D8");
        comboText.text = "x" + combo.ToString();
    }

    private IEnumerator AnimateComboPop()
    {
        RectTransform comboRect = comboText.rectTransform;
        Vector3 targetScale = comboOriginalScale * 1.15f;
        float t = 0f;
        float duration = 0.1f;

        while (t < duration)
        {
            t += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(comboOriginalScale, targetScale, t / duration);
            yield return null;
        }

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(targetScale, comboOriginalScale, t / duration);
            yield return null;
        }

        comboRect.localScale = comboOriginalScale;
    }
}
