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
        PlayerPrefs.SetInt("SlotSaved" + SaveID.saveID, 1);
        PlayerPrefs.SetInt("LoadSaved" + SaveID.saveID, 1);
        PlayerPrefs.SetInt("SavedScene" + SaveID.saveID, SceneManager.GetActiveScene().buildIndex);
        playerPosData.PlayerPosSave();
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Save()
        {
        PlayerPrefs.SetInt("SlotSaved" + SaveID.saveID, 1);
        PlayerPrefs.SetInt("LoadSaved" + SaveID.saveID, 1);
        PlayerPrefs.SetInt("SavedScene" + SaveID.saveID, SceneManager.GetActiveScene().buildIndex);
        playerPosData.PlayerPosSave();
        }
}

