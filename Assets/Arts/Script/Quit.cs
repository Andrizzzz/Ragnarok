using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    SavePlayerPos playerPosData;

    void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        playerPosData.PlayerPosSave();
        SceneManager.LoadScene("Main Menu");
    }
    public void Save()
        {
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            playerPosData.PlayerPosSave();
        }
}

