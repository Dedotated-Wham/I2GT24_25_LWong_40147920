using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Destroy gameObject upon collisions.
    private void OnTriggerEnter(Collider other)

    {   
        //Check if the other tag was the Player, if it was remove a life.
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Game Over Player");
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        //Check if the other tag was the Animal, if so add points to the score.
        else if (other.CompareTag("Animal"))
        {
            //gameManager.AddScore(5);
            //Destroy(other.gameObject);
            other.GetComponent<AnimalHunger>().FeedAnimal(1);
            Destroy(gameObject);
            
        }
    }
}
