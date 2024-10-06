using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for using TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    private float timeRemaining = 9 * 60; // Start time in seconds (9 minutes)

    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Decrease the time remaining
                timeRemaining -= Time.deltaTime;

                // Convert the remaining time to minutes and seconds
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);

                // Update the timer text in the format "mm:ss"
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                // If the timer reaches zero, stop the timer
                timeRemaining = 0;
                timerIsRunning = false;

                // Optionally: Do something when the timer reaches zero
                Debug.Log("Time's up!");
            }
        }
    }
}
