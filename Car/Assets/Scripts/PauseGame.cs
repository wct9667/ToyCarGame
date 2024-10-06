using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public Canvas pauseScreen;
    public Button resumeButton;
    public Button quitButton;


    // Start is called before the first frame update
    void Awake()
    {
        paused = false;
        pauseScreen.enabled = paused;
    }

    // Update is called once per frame
    void Update()
    {
        pauseScreen.enabled = paused;
        //pauses game
#if (UNITY_EDITOR)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
#endif
        if (paused)
        {
            //only shows mouse if game is paused
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //doesnt work if escape is hit to unpause bc unity editor shenanigans
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    /// <summary>
    /// toggles and shows pause screen
    /// </summary>
    /// <returns></returns>
    public void TogglePause()
    {
        paused = !paused;
        pauseScreen.enabled = paused;
        if (paused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Respawn()
    {
        //loading.text = ("Loading Level: " + SceneManager.GetActiveScene().name);
        //paused = false;
        //Time.timeScale = 1f;
        //pauseScreen.enabled = paused;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void QuitToMenu()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseScreen.enabled = paused;
        SceneManager.LoadScene("StartScreen");
    }
}