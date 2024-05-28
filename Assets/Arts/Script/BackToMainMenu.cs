using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class BackToMainMenu : MonoBehaviour
    {
        // Function to be called when the button is clicked
        public void GoToMainMenu()
        {
            // Load the main menu scene by its name
            SceneManager.LoadScene("Main Menu");
        }
    }
}
