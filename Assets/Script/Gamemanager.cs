using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // =========================
    // SCENE CHANGE
    // =========================

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }

    // =========================
    // RESTART CURRENT SCENE
    // =========================

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    // =========================
    // EXIT GAME
    // =========================

    public void ExitGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Game Exit");

        Application.Quit();
    }
}
