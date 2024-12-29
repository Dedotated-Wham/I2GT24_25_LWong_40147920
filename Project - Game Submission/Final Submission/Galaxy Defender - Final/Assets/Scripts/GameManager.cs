using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;              //Reference PlayerController script.

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    public GameObject fireRateCountDownText;
    public TextMeshProUGUI speedBoostCountDownText;


    private int score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;                                     //Set score to 0 initially.
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;

        fireRateCountDownText.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasPowerUpFireRate)
        {
            Debug.Log("Player has Increased Fire Rate");
            fireRateCountDownText.SetActive(true);
            //fireRateCountDownText.text = "Increased Fire Rate: " + playerController.FireRateCountdown;
        }
    }

    public void UpdateScore(int scoreToAdd)            //public void to let other scripts see it.
    {
        
        score += scoreToAdd;                           //Adds whatever value the int scoreToAdd is onto the current score value.
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;
    }

}
