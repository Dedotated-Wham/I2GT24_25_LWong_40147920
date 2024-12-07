using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    private int score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;                                     //Set score to 0 initially.
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)            //public void to let other scripts see it.
    {
        
        score += scoreToAdd;                           //Adds whatever value the int scoreToAdd is onto the current score value.
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;
    }

}
