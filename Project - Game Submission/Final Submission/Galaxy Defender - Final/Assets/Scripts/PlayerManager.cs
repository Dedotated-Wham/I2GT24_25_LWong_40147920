using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public static bool isLevelComplete;
    public GameObject levelCompleteScreen;

    public GameObject crosshairUI;

    [SerializeField] private CinemachineDollyCart dollyCart;  // Make sure the reference is set up via Inspector

    private void Start()
    {
        if (dollyCart != null)
        {
            Debug.Log("Successfully found CinemachineDollyCart.");
        }
        else
        {
            Debug.LogError("CinemachineDollyCart not assigned.");
        }
    }
    
    private void Awake()
    {
        isGameOver = false;
        isLevelComplete = false;
    }
    private void Update()
    {
        // Stop the Dolly Cart movement when game is over or level is complete
        if (isGameOver || isLevelComplete)
        {
            StopDollyCartMovement();
        }

        if (isGameOver && !gameOverScreen.activeSelf)
        {
            // Start coroutine to handle game over actions after a delay
            StartCoroutine(HandleGameOver());
        }

        if (isLevelComplete && !levelCompleteScreen.activeSelf)
        {
            // Start coroutine to handle level completion actions after a delay
            StartCoroutine(HandleLevelComplete());
        }

    }
    private void StopDollyCartMovement()
    {
        if (dollyCart != null)
        {
            dollyCart.m_Speed = 0f;  // Set speed to 0 to stop the movement
            Debug.Log("Dolly Cart speed set to: " + dollyCart.m_Speed);
        }

    }

    // Coroutine to handle the game over logic with a delay
    private IEnumerator HandleGameOver()
    {
        // Wait for 1 second before executing the actions
        yield return new WaitForSeconds(2f);

        gameOverScreen.SetActive(true);
        crosshairUI.SetActive(false);
        Time.timeScale = 0f;  // Pause the game
    }

    // Coroutine to handle the level completion logic with a delay
    private IEnumerator HandleLevelComplete()
    {
        // Wait for 1 second before executing the actions
        yield return new WaitForSeconds(2f);

        levelCompleteScreen.SetActive(true);
        crosshairUI.SetActive(false);
        Time.timeScale = 0f;  // Pause the game
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
