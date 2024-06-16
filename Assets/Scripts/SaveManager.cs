using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// to allow write inside folders
using System.IO;
//allow to serialize and deserialize objects in game
using System.Runtime.Serialization;
//to dake and deserialize our binary file to save our data
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager
{
    public static string _dirName = "SaveData";
    public static string _fileName = "SaveFile.txt";

    public static void Save(SaveData saveData)
    {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + _dirName);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(GetFilePathName());
        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
        // Show full route of the file
        Debug.Log(GetFilePathName());
    }

    public static SaveData Load()
    {
        SaveData saveData;

        if (!SavedataExists())
        {
            saveData = null;
            Debug.Log("Failed to load data");
        }
        else
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream loadFile = File.Open(GetFilePathName(), FileMode.Open);
                // casting: allow to turn this into a SaveData object type
                saveData = (SaveData)binaryFormatter.Deserialize(loadFile);
                loadFile.Close();

            }
            catch (SerializationException)
            {
                saveData = null;
                Debug.Log("Failed to load data");
            }
        }
        // we must return SaveData type
        return saveData;
    }


    public static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + _dirName);
    }
    public static string GetFilePathName()
    {
        return Application.persistentDataPath + "/" + _dirName + "/" + _fileName;
    }

    public static bool SavedataExists()
    {
        return File.Exists(GetFilePathName());
    }
}