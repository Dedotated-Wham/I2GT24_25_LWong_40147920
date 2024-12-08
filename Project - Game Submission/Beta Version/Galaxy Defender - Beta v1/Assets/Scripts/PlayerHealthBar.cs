using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;               //Reference PlayerHealth script.
    public Image fillImage;
    private Slider slider;

  
    void Awake()
    {
        slider = GetComponent<Slider>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
            //PlayerManager.isGameOver = true;                    //Game Over Sreen Display when Health becomes 0.
        }
        float fillvalue = playerHealth.currentHealth / playerHealth.maxHealth;
        slider.value = fillvalue;
    }
}
