using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private AsyncOperation _asyncSceneLoad;
    private bool _showsStartButton = false;

    [SerializeField] private GameObject _startButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _asyncSceneLoad = SceneManager.LoadSceneAsync("TheLevel_01", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (_asyncSceneLoad.isDone && !_showsStartButton)
        {
            _showsStartButton = true;
            _startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("LoadingScreen");
        EventManager.Instance.OnGameSpeedChange(true);
    }
}
