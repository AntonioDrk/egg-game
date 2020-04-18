using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartLastLevel()
    {
        LevelData data = SaveSystem.LoadLevelData();
        if(data != null)
        {
            StartLevel(data.CurrentLvl);
        }
        // If there's no save (it's the first time you play the game)
        else
        {
            // Load the first level
            StartLevel(1);
        }
    }

    public void StartLevel(int k)
    {
        // Delete any saved checkpoint from the last time you played
        SaveSystem.DeleteCheckpointData();
        LoadScene(k);
    }

    public void LoadScene(int k)
    {
        SceneManager.LoadScene(k);
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
