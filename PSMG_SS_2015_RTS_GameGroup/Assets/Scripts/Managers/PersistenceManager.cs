using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// The persistence manager is a subcomponent of the game manager and
/// is responsible for storing and loading save games.
/// </summary>

public class PersistenceManager : MonoBehaviour
{

    public const string STORAGE_PATH = "/savedGames.gd";

    private List<SaveGame> savedGames;
    private SaveGame currentGame;

    private List<PersistenceManagerListener> listeners;

    public interface PersistenceManagerListener
    {
        void OnGameSaved();
        void OnGameLoaded();
    }

    private void Awake()
    {
        savedGames = new List<SaveGame>();
        listeners = new List<PersistenceManagerListener>();
    }

    public void RegisterListener(PersistenceManagerListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(PersistenceManagerListener listener)
    {
        listeners.Remove(listener);
    }

    public void SaveGame()
    {
        savedGames.Add(currentGame);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + STORAGE_PATH);
        binaryFormatter.Serialize(fileStream, savedGames);
        fileStream.Close();

        // TODO wait for fileWriting to be completed
        foreach (PersistenceManagerListener listener in listeners)
        {
            listener.OnGameSaved();
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + STORAGE_PATH))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + STORAGE_PATH, FileMode.Open);
            savedGames = (List<SaveGame>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

        // TODO wait for fileReading to be completed
        foreach (PersistenceManagerListener listener in listeners)
        {
            listener.OnGameLoaded();
        }
    }

}