using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; set; }
    private float startTime;

    void Awake()
    {
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            ResetTime();
        }
    }

    private void Update()
    {
        var levelFinished = GameManager.Instance.levelFinished;
        if (SceneManager.GetActiveScene().buildIndex > 2 && !levelFinished)
        {
            var time = GetTime();

            string timeString = GetTimeString(time);

            GameManager.Instance.UpdateTimeText(timeString);
        }
    }

    public void ResetTime()
    {
        startTime = Time.time;
    }

    public float GetTime()
    {
        var time = Time.time - startTime;
        return time;
    }

    public string GetTimeString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        string timeString = "";
        if (seconds < 10)
        {
            timeString = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timeString = minutes.ToString() + ":" + seconds.ToString();
        }
        return timeString;
    }
}
