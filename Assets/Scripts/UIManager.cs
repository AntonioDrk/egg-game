using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        //File.Delete(Application.persistentDataPath + "Stats.txt");
    }

    private void Start()
    {
        // Stats scene
        if (SceneManager.GetActiveScene().buildIndex == 2)
            LoadStatsUI();
    }

    public void StartLastLevel()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
        LevelData data = SaveSystem.LoadLevelData();
        StartLevel(data.lastLevel);
    }

    public void StartLevel(int k)
    {
        LoadScene(k + 2);
    }

    public void LoadScene(int k)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
        // Delete any saved checkpoint from the last time you played
        SaveSystem.DeleteCheckpointData();
        TimerController.Instance.ResetTime();
        SceneManager.LoadScene(k);
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
        Application.Quit();
    }

    public void LoadStatsUI()
    {
        StatsData data = SaveSystem.LoadStatsData();
        GameObject.Find("EggDrop").GetComponent<TextMeshProUGUI>().text = data.eggDrop.ToString();
        int deaths = data.deathByLaser + data.deathByFalling + data.eggDrop;
        GameObject.Find("Killed").GetComponent<TextMeshProUGUI>().text = deaths.ToString();
        GameObject.Find("Falling").GetComponent<TextMeshProUGUI>().text = data.deathByFalling.ToString();
        GameObject.Find("Laser").GetComponent<TextMeshProUGUI>().text = data.deathByLaser.ToString();

        if(data.slowestWin != 0)
        {
            GameObject.Find("Slow").GetComponent<TextMeshProUGUI>().text = TimerController.Instance.GetTimeString(data.slowestWin);
            GameObject.Find("Fast").GetComponent<TextMeshProUGUI>().text = TimerController.Instance.GetTimeString(data.fastestWin);
        }
        else
        {
            GameObject.Find("Slow").GetComponent<TextMeshProUGUI>().text = "-";
            GameObject.Find("Fast").GetComponent<TextMeshProUGUI>().text = "-";
        }
    }
}
