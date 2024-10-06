using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] _images;
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    private float timeRemaining = 9 * 60; // Start time in seconds (9 minutes)

    private bool timerIsRunning = true;

    private int score = 0;
    void Start()
    {
        foreach (UnityEngine.UI.Image image in _images)
        {
            image.enabled = false;
        }

        _images[0].enabled = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 1)
            {
                // Decrease the time remaining
                timeRemaining -= Time.deltaTime;

                // Convert the remaining time to minutes and seconds
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);

                // Update the timer text in the format "mm:ss"
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                if (score == 9)
                {
                    SceneManager.LoadScene("WinScreen");
                }
            }
            else
            {
                // If the timer reaches zero, stop the timer
                timeRemaining = 0;
                timerIsRunning = false;

                // Optionally: Do something when the timer reaches zero
                Debug.Log("Time's up!");
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    public void ChangeScore()
    {
        score++;
        switch (score)
        {
            case 0:
                DisableImages();
                _images[0].enabled = true;
                break;
            case 1:
                DisableImages();
                _images[1].enabled = true;
                break;
            case 2:
                DisableImages();
                _images[2].enabled = true;
                break;
            case 3:
                DisableImages();
                _images[3].enabled = true;
                break;
            case 4:
                DisableImages();
                _images[4].enabled = true;
                break;
            case 5:
                DisableImages();
                _images[5].enabled = true;
                break;
            case 6:
                DisableImages();
                _images[6].enabled = true;
                break;
            case 7:
                DisableImages();
                _images[7].enabled = true;
                break;
            case 8:
                DisableImages();
                _images[8].enabled = true;
                break;
            case 9:
                DisableImages();
                _images[9].enabled = true;
                break;
        }
    }

    private void DisableImages()
    {
        foreach (UnityEngine.UI.Image image in _images)
        {
            image.enabled = false;
        }
    }
}
