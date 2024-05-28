using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject additionalPanel;
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
        additionalPanel.SetActive(false);
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

    public void OpenAdditionalPanel()
    {
        if (CanProcessButtonClick())
        {
            additionalPanel.SetActive(true);
            additionalPanel.transform.SetAsLastSibling();  // Bring the panel to the front
            Debug.Log("Additional panel opened.");
        }
    }

    public void CloseAdditionalPanel()
    {
        if (CanProcessButtonClick())
        {
            additionalPanel.SetActive(false);
            Debug.Log("Additional panel closed.");
        }
    }

    public void BackToPauseMenu()
    {
        if (CanProcessButtonClick())
        {
            additionalPanel.SetActive(false);
            pauseMenu.SetActive(true);
            Debug.Log("Returned to pause menu.");
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
