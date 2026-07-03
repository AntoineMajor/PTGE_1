using UnityEngine;
using TMPro; // Required for TextMesh Pro
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Drag your TMP object here
    private int score = 0;
    private float tickRate = 0.1f; // Tenth of a second

    void Start()
    {
        if (scoreText != null)
        {
            StartCoroutine(TickScore());
        }
    }

    IEnumerator TickScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}