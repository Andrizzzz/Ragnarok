using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class MainStory : MonoBehaviour
    {
      
        void OnEnable()
        {
            SceneManager.LoadScene("Level1-1", LoadSceneMode.Single);
        }
    }
}
