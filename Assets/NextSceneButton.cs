using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class NextSceneButton : MonoBehaviour
    {
        // Public function to be called when the button is clicked
        public void LoadNextScene()
        {
            // Get the current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Load the next scene (increment the current scene index)
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
