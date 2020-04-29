using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /// functions for the levels data
    public static void SaveLevelData(int currentLevel = 1, int currentStars = 0)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "Levels.txt";

        if (File.Exists(path))
        {
            LevelData data = LoadLevelData();
            data.SaveLevelData(currentLevel, currentStars);

            FileStream stream = new FileStream(path, FileMode.Open);       
            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            LevelData data = new LevelData();

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static LevelData LoadLevelData()
    {
        string path = Application.persistentDataPath + "Levels.txt";

        if (!File.Exists(path))
            SaveSystem.SaveLevelData();

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        LevelData data = formatter.Deserialize(stream) as LevelData;

        stream.Close();

        return data;
    }

    /// functions for the last checkpoint data
    public static void SaveCheckpointData(Vector3 checkpointPosition, int id, Vector3 eggPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "Checkpoint.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        CheckpointData data = new CheckpointData(checkpointPosition, id, eggPosition);

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
    
    /// functions for the statistics data
    public static void SaveStatsData(int eggDrop, int killed)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "Stats.txt";

        if (File.Exists(path))
        {
            StatsData data = LoadStatsData();
            data.IncreaseVal(eggDrop, killed);

            FileStream stream = new FileStream(path, FileMode.Open);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            StatsData data = new StatsData();
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static StatsData LoadStatsData()
    {
        string path = Application.persistentDataPath + "Stats.txt";

        if (!File.Exists(path))
            SaveStatsData(0, 0);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        StatsData data = formatter.Deserialize(stream) as StatsData;
        stream.Close();

        return data;
    }

}
