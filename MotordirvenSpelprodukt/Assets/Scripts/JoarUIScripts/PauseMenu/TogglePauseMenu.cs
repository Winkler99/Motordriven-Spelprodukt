using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] GameInput _gameInput;

    private void Start()
    {

        _gameInput.OnPauseButtonPressed += GameInput_OnPauseButtonPressed;
    }

    private void GameInput_OnPauseButtonPressed(object sender, System.EventArgs e)
    {
        Debug.Log("OnPauseButtonPressed");
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    // Freezes the game
        GameIsPaused = true;
    }
}
