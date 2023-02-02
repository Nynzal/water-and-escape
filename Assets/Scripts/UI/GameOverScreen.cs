using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _endScreenOverlay;

    private void OnEnable()
    {
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.GameOver -= OnGameOver;
    }

    public void OnGameEndButton()
    {
        Application.Quit();
    }

    private void OnGameOver()
    {
        _endScreenOverlay.SetActive(true);
    }
}
