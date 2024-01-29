using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI score;
    Player player;

    int goalScore = 0;

    float currentScore = 0.0f;

    public float scoreUpSpeed = 50.0f;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        player.onScoreChange += RefreshScore;

        goalScore = 0;
        currentScore = 0.0f;
        score.text = "Score\n00000\n\n\n\n\nA : ←\nB : →";
    }
    private void Update()
    {
        if (currentScore < goalScore)
        {
            float speed = Mathf.Max((goalScore - currentScore) * 5.0f, scoreUpSpeed);   // 최소 scoreUpSpeed 보장

            currentScore += Time.deltaTime * speed;
            currentScore = Mathf.Min(currentScore, goalScore);

            int temp = (int)currentScore;
            score.text = $"Score\n{temp:d5}\n\n\n\n\nA : ←\nB : →";
            
        }
    }

    private void RefreshScore(int newScore)
    {
        goalScore = newScore;
    }
}
