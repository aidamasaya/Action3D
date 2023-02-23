using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Text GameOverText;

    [SerializeField] Text ScoreText;
    int _currentScore = 00000;

    void Start()
    {
        GameOverText.gameObject.SetActive(false);

        ScoreText.text = _currentScore.ToString();
    }
    
    public void ShowGameOver()
    {
        GameOverText.gameObject.SetActive(true);
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        ScoreText.text = _currentScore.ToString();
    }
}
