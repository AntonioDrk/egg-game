using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        //File.Delete(Application.persistentDataPath + "Levels.txt");
    }

    public void StartLastLevel()
    {
        LevelData data = SaveSystem.LoadLevelData();
        StartLevel(data.lastLevel);
    }

    public void StartLevel(int k)
    {
        LoadScene(k + 2);
    }

    public void LoadScene(int k)
    {
        // Delete any saved checkpoint from the last time you played
        SaveSystem.DeleteCheckpointData();
        SceneManager.LoadScene(k);
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
