using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public GameObject LoseScreen;
    public TimerScript Timer;

    private void Awake()
    {
        Timer = FindAnyObjectByType<TimerScript>();
    }
    private void Update()
    {
        if(Timer != null)
        {
            if (Timer.timeLeft <=0 ) 
            {
                LoseScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void Retry()
    {
        Debug.Log("Restarting level");
        LoseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log($"Time scale currently: {Time.timeScale}");
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
