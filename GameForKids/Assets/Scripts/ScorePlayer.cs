using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    public static ScorePlayer scorePlayer;
    public void Start()
    {
        scorePlayer = this;
        PlayerPrefs.SetInt("Score", 100);
    }

    [SerializeField] private TextMeshProUGUI score;
    internal bool notAffordable = false;

    public void CurrentScore()
    {
        if(notAffordable)
        {
            score.text = "You cannot afford that(";
        }
        score.text ="Your balance: " + PlayerPrefs.GetInt("Score", 0).ToString() + "$";
    }

    public int GetCurrent()
    {
        
        return PlayerPrefs.GetInt("Score", 0);
    }

    public void ChangeScore(int score)
    {
        int oldScore = PlayerPrefs.GetInt("Score", 0);

        PlayerPrefs.SetInt("Score", score + oldScore);
    }
    public void Bought(int score)
    {
        int oldScore = PlayerPrefs.GetInt("Score", 0);

        PlayerPrefs.SetInt("Score", oldScore - score);
    }

    void Update()
    {
        CurrentScore();
    }

    public void NewText()
    {
        score.text = "You cannot afford that(";
    }
}
