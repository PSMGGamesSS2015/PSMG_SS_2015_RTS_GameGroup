using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Persistence;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The persistence manager is a subcomponent of the game manager and
    /// is responsible for storing and loading save games.
    /// </summary>

    public class PersistenceManager : MonoBehaviour
    {

        public const string StoragePath = "/savedGames.gd";

        private List<SaveGame> savedGames;
        // TODO
        private SaveGame currentGame;

        private List<IPersistenceManagerListener> listeners;

        public interface IPersistenceManagerListener
        {
            void OnGameSaved();
            void OnGameLoaded();
        }

        public void Awake()
        {
            savedGames = new List<SaveGame>();
            listeners = new List<IPersistenceManagerListener>();
        }

        public void RegisterListener(IPersistenceManagerListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IPersistenceManagerListener listener)
        {
            listeners.Remove(listener);
        }

        public void SaveGame()
        {
            savedGames.Add(currentGame);
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Create(Application.persistentDataPath + StoragePath);
            binaryFormatter.Serialize(fileStream, savedGames);
            fileStream.Close();

            // TODO wait for fileWriting to be completed
            foreach (var listener in listeners)
            {
                listener.OnGameSaved();
            }
        }

        public void LoadGame()
        {
            if (File.Exists(Application.persistentDataPath + StoragePath))
            {
                var binaryFormatter = new BinaryFormatter();
                var fileStream = File.Open(Application.persistentDataPath + StoragePath, FileMode.Open);
                savedGames = (List<SaveGame>)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }

            // TODO wait for fileReading to be completed
            foreach (var listener in listeners)
            {
                listener.OnGameLoaded();
            }
        }

    }
}