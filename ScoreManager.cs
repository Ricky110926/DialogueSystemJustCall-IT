using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private static int score = 0;
    [SerializeField]
    private static int totalScore = 0;

    public int getScore() { score = PlayerPrefs.GetInt("LevelScore"); return score;}
    public void resetScore() {score = 0; PlayerPrefs.DeleteKey("LevelScore"); }
    public void setScore(int addScore) {score = addScore + getScore(); Debug.Log(score); PlayerPrefs.SetInt("LevelScore", score); }
    public void setTotalScore(int addTotalScore){totalScore = addTotalScore + getTotalScore(); PlayerPrefs.SetInt("TotalScore", totalScore); }
    public int getTotalScore() { totalScore = PlayerPrefs.GetInt("TotalScore"); return totalScore;}
    public void resetTotalScore() { totalScore = 0; PlayerPrefs.DeleteKey("TotalScore"); }

    public void removeLevelScore()
    {
        if (getTotalScore() > 0)
        {
            totalScore = getTotalScore() - getScore();
            PlayerPrefs.SetInt("TotalScore", totalScore);
        }
    }
}
