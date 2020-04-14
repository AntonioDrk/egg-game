using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveCheckpointData(Vector3 checkPointPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "Checkpoint.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        CheckpointData data = new CheckpointData(checkPointPosition);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CheckpointData LoadCheckpointData()
    {
        string path = Application.persistentDataPath + "Checkpoint.txt";
        
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CheckpointData data = formatter.Deserialize(stream) as CheckpointData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Checkpoint file not found in " + path);
            return null;
        }
    }

}
