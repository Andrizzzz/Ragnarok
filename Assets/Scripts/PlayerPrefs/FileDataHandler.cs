using System;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public PlayerStatsData Load()
    {
        string fullPath = System.IO.Path.Combine(dataDirPath, dataFileName);
        PlayerStatsData loadedData = null;
        if (System.IO.File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (System.IO.FileStream stream = new System.IO.FileStream(fullPath, System.IO.FileMode.Open))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<PlayerStatsData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(PlayerStatsData data)
    {
        string fullPath = System.IO.Path.Combine(dataDirPath, dataFileName);
        try
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (System.IO.FileStream stream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
