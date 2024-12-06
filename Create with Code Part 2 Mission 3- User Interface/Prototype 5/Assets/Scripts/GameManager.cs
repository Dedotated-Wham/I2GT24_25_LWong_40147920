using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());        //Uses the IEnumerator below.
        score = 0;                              //Set score to 0 initially.
        UpdateScore(0);                         //Using the method void UpdateScore we created below. Setting the initial int to 0.

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
        
    }

    public void UpdateScore(int scoreToAdd)            //public void to let other scripts see it.
    {
        score += scoreToAdd;                           //Adds whatever value the int scoreToAdd is onto the current score value.
        scoreText.text = "Score: " + score;
    }

    public void GameOver()                            //Can call on this void gameOver Function in script.
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);                 //Reload current scene player is in.
    }
}
