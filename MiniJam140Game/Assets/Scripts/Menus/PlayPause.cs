using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPause : MonoBehaviour
{
    //set scene build index of pause menu
    public GameObject PauseMenu;
    bool Paused = false;

    // Update is called once per frame
    void Update()
    {
        //if player presses ESC, set pause menu active
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Debug.Log("Resuming");
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;

    }
    public void MainMenu()
    {
        Debug.Log("Going to main menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
