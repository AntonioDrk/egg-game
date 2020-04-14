using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /// functions for the levels data
    public static void SaveLevelData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "Levels.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevelData()
    {
        string path = Application.persistentDataPath + "Levels.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        return null;
    }

    /// functions for the last checkpoint data
    public static void SaveCheckpointData(Vector3 checkpointPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "Checkpoint.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        CheckpointData data = new CheckpointData(checkpointPosition);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CheckpointData LoadCheckpointData()
    {
        string path = Application.persistentDataPath + "Checkpoint.txt";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CheckpointData data = formatter.Deserialize(stream) as CheckpointData;
            stream.Close();

            return data;
        }
        return null;
    }
    
    public static void DeleteCheckpointData()
    {
        string path = Application.persistentDataPath + "Checkpoint.txt";
        
        if (File.Exists(path))
        {
            File.Delete(path);
            #if UNITY_EDITOR
               UnityEditor.AssetDatabase.Refresh();
            #endif
        }
    }
}
