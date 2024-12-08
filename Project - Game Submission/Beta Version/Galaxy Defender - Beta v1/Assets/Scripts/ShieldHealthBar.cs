using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHealthBar : MonoBehaviour
{
    public ShieldHealth playerHealth;               //Reference ShieldHealth script.
    public Image fillImage;
    private Slider slider;

    private PlayerController player;         //Reference PlayerController script.
    void Awake()
    {
        slider = GetComponent<Slider>();
        fillImage.enabled = false;             //Shield Bar to be disabled at start before getting power up.
    }


    // Update is called once per frame
    void Update()
    {
        // Use shield first before health.
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
            //PlayerManager.isGameOver = true;                    //Game Over Sreen Display when Health becomes 0.
        }
        float fillvalue = shieldHealth.currentHealth / shieldHealth.maxHealth;
        slider.value = fillvalue;
    }
}
