using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;            //Reference to UI Crosshair Image in Canvas.
    public Transform playerTransform;       //Reference to Player's Position
    public Camera mainCamera;               //Main camera to convert world position to screen position
    public float movementFactor = 1.0f;     //Controls how much the crosshair moves based on player movement

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = crosshairImage.GetComponent<RectTransform>();

        CentreCrosshair();
    }

    // Update is called once per frame
    void Update()
    {
        // Update crosshair position based on the player's world position
        UpdateCrosshairPosition();
    }

    private void CentreCrosshair()
    {
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Center anchor point
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // Center anchor point
        rectTransform.anchoredPosition = Vector2.zero; // Position it at the center
    }

    // Update the crosshair position based on the player's movement
    private void UpdateCrosshairPosition()
    {
        // Convert player's world position to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerTransform.position);

        // Get the canvas size in screen space (in pixels)
        Vector2 canvasSize = rectTransform.root.GetComponent<RectTransform>().sizeDelta;

        // Normalize the player's screen position relative to the center of the screen
        float normalizedX = (screenPosition.x - Screen.width / 2) / (Screen.width / 2); // Normalize between -1 and 1
        float normalizedY = (screenPosition.y - Screen.height / 2) / (Screen.height / 2); // Normalize between -1 and 1

        // Calculate the offset in canvas coordinates based on normalized position
        float offsetX = normalizedX * (canvasSize.x / 2) * movementFactor;
        float offsetY = normalizedY * (canvasSize.y / 2) * movementFactor;

        // Smooth the movement of the crosshair
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(offsetX, offsetY), Time.deltaTime * 10f);
    }
}