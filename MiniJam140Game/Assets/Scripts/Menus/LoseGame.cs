using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public GameObject LoseScreen;
    public void Retry()
    {
        Debug.Log("Restarting level");
        LoseScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log($"Time scale currently: {Time.timeScale}");
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
