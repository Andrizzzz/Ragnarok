using System;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private PlayerStatsData playerStatsData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadPlayerStats();
    }

    public void NewGame()
    {
        this.playerStatsData = new PlayerStatsData();
    }

    public void LoadPlayerStats()
    {
        this.playerStatsData = dataHandler.Load();

        if (this.playerStatsData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(playerStatsData);
        }
    }

    public void SavePlayerStats()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(playerStatsData);
        }

        dataHandler.Save(playerStatsData);
    }

    private void OnApplicationQuit()
    {
        SavePlayerStats();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        List<IDataPersistence> dataPersistenceObjects = new List<IDataPersistence>();
        var allObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in allObjects)
        {
            if (obj is IDataPersistence)
            {
                dataPersistenceObjects.Add(obj as IDataPersistence);
            }
        }
        return dataPersistenceObjects;
    }
}
