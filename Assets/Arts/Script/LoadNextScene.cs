using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{


    public class LoadNextScene : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("Level1-1");
        }
    }
}
