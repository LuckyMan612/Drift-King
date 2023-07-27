using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Reference for Pause UI
    public GameObject pauseMenu;
    public GameObject hud;

    // Variable to track if the game is paused
    private bool isPaused = false;

    // Remember the original time scale before pausing
    private float originalTimeScale;

    private void Start()
    {
        // Ensure the pauseCanvas is disabled at the beginning
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        // Don't allow pausing if out of gas
        if (CarFuel.Instance.outOfGas)
        {
            return;
        }

        // Check for the pause input
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the pause state
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        // Save the original time scale
        originalTimeScale = Time.timeScale;

        // Pause the game
        Time.timeScale = 0f;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        hud.SetActive(false);



        // Set the pause state to true
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = originalTimeScale;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        hud.SetActive(true);

        // Set the pause state to false
        isPaused = false;
    }
}
