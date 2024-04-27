using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject[] newGameButtons = new GameObject[3];
    public GameObject[] loadGameButtons = new GameObject[3];

    public int[] saveIds = new int[3];
    private void Update()
    {
        if (PlayerPrefs.GetInt("SlotSaved" + saveIds[0]) == 1)
        {
            loadGameButtons[0].SetActive(true);
            newGameButtons[0].SetActive(false);
        }
        else
        {
            loadGameButtons[0].SetActive(false);
            newGameButtons[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("SlotSaved" + saveIds[1]) == 1)
        {
            loadGameButtons[1].SetActive(true);
            newGameButtons[1].SetActive(false);
        }
        else
        {
            loadGameButtons[1].SetActive(false);
            newGameButtons[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("SlotSaved" + saveIds[2]) == 1)
        {
            loadGameButtons[2].SetActive(true);
            newGameButtons[2].SetActive(false);
        }
        else
        {
            loadGameButtons[2].SetActive(false);
            newGameButtons[2].SetActive(true);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey("p_x" + SaveID.saveID);
        PlayerPrefs.DeleteKey("p_y" + SaveID.saveID);
        PlayerPrefs.DeleteKey("p_z" + SaveID.saveID);
        PlayerPrefs.DeleteKey("TimeToLoad" + SaveID.saveID);
        PlayerPrefs.DeleteKey("Saved");
        SceneManager.LoadScene("Level1-1");
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        if(PlayerPrefs.GetInt("LoadSaved" + SaveID.saveID) == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene" + SaveID.saveID));
        }
        else
        {
            return;
        }
        
        Time.timeScale = 1;
    }
    public void SetSaveID(int saveID)
    {
        SaveID.saveID = saveID;
    }
    public void ClearSave(int saveID)
    {
        PlayerPrefs.DeleteKey("SlotSaved" + saveID);
    }
}