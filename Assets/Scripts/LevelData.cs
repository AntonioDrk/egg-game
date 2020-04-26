using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData
{
    public int lastLevel;
    public int[] stars;

    public LevelData()
    {
        lastLevel = 1;
        stars = new int[10];
    }

    public void SaveLevelData(int currentLevel, int currentStars)
    {
        Debug.Log("Current level & stars: " + currentLevel + " " + currentStars);
        if (currentStars > stars[currentLevel])
            stars[currentLevel] = currentStars;

        if (currentLevel == lastLevel)
            lastLevel++;
    }
}
