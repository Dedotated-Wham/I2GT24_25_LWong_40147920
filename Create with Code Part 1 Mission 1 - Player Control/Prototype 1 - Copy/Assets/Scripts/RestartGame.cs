using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //When you want to restart the game and reset the scene by pressing a key. "R" in this case in the code below.
        if (Input.GetKeyDown(KeyCode.R)) { 
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }    
    }
}
