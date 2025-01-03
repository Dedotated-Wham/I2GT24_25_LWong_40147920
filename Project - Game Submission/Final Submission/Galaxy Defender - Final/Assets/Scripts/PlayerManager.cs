using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public static bool isLevelComplete;
    public GameObject levelCompleteScreen;

    public GameObject crosshairUI;

    private void Awake()
    {
        isGameOver = false;
        isLevelComplete = false;
    }

    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            crosshairUI.SetActive(false);
            Time.timeScale = 0f;  // Pause the game
        }

        if(isLevelComplete)
        {
            levelCompleteScreen.SetActive(true);
            crosshairUI.SetActive(false);
            Time.timeScale = 0f;  // Pause the game
        }
    }

    public void ReplayLevel() //Replays level.
    {
        Time.timeScale = 1f;  // Resume the game before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetGameFlags();  // Reset game over or level complete flags
    }

    public void MainMenu() //Goes back to start/main menu.
    {
        Time.timeScale = 1f;  // Resume the game before reloading
        SceneManager.LoadScene(0);
        ResetGameFlags();  // Reset game over or level complete flags
    }
    private void ResetGameFlags()
    {
        isGameOver = false;
        isLevelComplete = false;
        gameOverScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);
        crosshairUI.SetActive(true);
    }
}
