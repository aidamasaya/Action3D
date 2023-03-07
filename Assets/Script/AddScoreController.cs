using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddScoreController: MonoBehaviour
{
    [SerializeField] float _scoreChangeInterval = 0.5f;
    Text _scoreText = default;

    int _maxScore = 99999999;
    int _currentScore = 0;

    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    public void AddScore(int score)
    {
        int tempScore = _currentScore;
        _currentScore = Mathf.Min(_currentScore + score, _maxScore);
        Debug.Log(_currentScore);

        if (tempScore != _maxScore)
        {
            DOTween.To(() => tempScore,
            x => tempScore = x,
            _currentScore,
            _scoreChangeInterval)
            .OnUpdate(() => _scoreText.text = tempScore.ToString("00000000"))
            .OnComplete(() => _scoreText.text = _currentScore.ToString("00000000"));
        }
    }
}
