using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highscoreDisplay;

    private int score;
    private int highscore;
    void Start()
    {
        score = ScoreManager.Instance.GetScore;
        highscore = ScoreManager.Instance.GetHighscore;
        scoreDisplay.text = score.ToString();
        highscoreDisplay.text = highscore.ToString();
    }
}
