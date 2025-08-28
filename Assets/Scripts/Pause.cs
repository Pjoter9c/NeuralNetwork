using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private Canvas _pauseCanvas;
    private bool _paused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        _paused = !_paused;
        if (_paused)
        {
            _pauseCanvas.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            _pauseCanvas.enabled = false;
        }
    }

    public void Restart()
    {
        _pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
