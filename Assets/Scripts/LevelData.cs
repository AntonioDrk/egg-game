using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData
{
    public int CurrentLvl;

    public LevelData()
    {
        CurrentLvl = SceneManager.GetActiveScene().buildIndex;
    }
}
