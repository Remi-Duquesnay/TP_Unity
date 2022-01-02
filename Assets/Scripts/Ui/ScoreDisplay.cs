using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour, IScoreObserver
{
    [SerializeField] TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        ScoreManager.Instance.Attach(this);
    }
    public void OnScoreChange(int score)
    {
        scoreDisplay.text = score.ToString();
    }

    
}
