using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    private int score;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
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
        while(true)
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
}
