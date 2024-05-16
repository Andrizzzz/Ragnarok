using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class Wait : MonoBehaviour
    {
        public float wait_time = 5f; // Corrected from `-` to `=`

        void Start()
        {
            StartCoroutine(WaitForIntro()); // Method name corrected to `WaitForIntro`
        }

        IEnumerator WaitForIntro()
        {
            yield return new WaitForSeconds(wait_time);
            SceneManager.LoadScene("Main Menu"); // Corrected method call and added semicolon
        }
    }
}








