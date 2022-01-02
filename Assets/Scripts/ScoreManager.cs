using System.Collections.Generic;
public class ScoreManager : IScoreManager
{
    private static ScoreManager _instance;
    private List<IScoreObserver> _observers = new List<IScoreObserver>();

    public static ScoreManager Instance => _instance ?? (_instance = new ScoreManager());

    public int score = 0;
    public int highScore = 0;

    private ScoreManager()
    { }

    public int GetScore
    {
        get => score;
    }

    public int GetHighscore
    {
        get => highScore;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddToScore(int value)
    {
        score += value;
        if (score > highScore)
            highScore = score;
        Notify();
    }

    public void Attach(IScoreObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IScoreObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IScoreObserver o in _observers)
        {
            o.OnScoreChange(score);
        }
    }
}