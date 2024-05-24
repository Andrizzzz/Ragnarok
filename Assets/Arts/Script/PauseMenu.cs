using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;
    private Button currentlyPressedButton;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Home()
    {
        if (CanProcessButtonClick())
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void Resume()
    {
        if (CanProcessButtonClick())
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void Restart()
    {
        if (CanProcessButtonClick())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    private bool CanProcessButtonClick()
    {
        // If another button is already pressed, ignore this click
        if (currentlyPressedButton != null)
        {
            Debug.Log("Another button is already pressed. Ignoring click.");
            return false;
        }

        // Otherwise, proceed with the button click
        StartCoroutine(ButtonActionComplete());
        return true;
    }

    private IEnumerator ButtonActionComplete()
    {
        // Mark button as pressed and wait for a short delay
        yield return new WaitForSecondsRealtime(0.5f);
        currentlyPressedButton = null;
        Debug.Log("Button action completed. Ready for next click.");
    }

    public void OnButtonPress(Button button)
    {
        currentlyPressedButton = button;
    }
}
