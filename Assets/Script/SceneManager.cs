using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Text GameOverText;

    void Start()
    {
        GameOverText.gameObject.SetActive(false);
    }
    
    public void ShowGameOver()
    {
        GameOverText.gameObject.SetActive(true);
    }
}
