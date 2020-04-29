using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; } 

    public int Points { get; set; }

    [SerializeField]
    private Text instructionsText;
    [SerializeField]
    private Text pointsText; 
    [SerializeField]
    private Text timeText;

    public bool[] starsId;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Debug.LogError("There seem to be two GameManagers instances in this scene, this will cause unwanted behaviour!");
        }
        Instance = this;
        starsId = new bool[3];
    }

    private void Start()
    {
        LoadLastCheckpoint();
        UpdatePointsText();
        UpdateInstructionsText("");
    }

    private void LoadLastCheckpoint()
    {
        var player = GameObject.Find("Player");
        var egg = GameObject.Find("Egg");
        var stars = GameObject.Find("Stars");

        CheckpointData data = SaveSystem.LoadCheckpointData();
        // If you found the checkpoint data file
        if (data != null)
        {
            Points = data.points;
            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            egg.transform.position = new Vector3(data.eggPosition[0], data.eggPosition[1], data.eggPosition[2]);

            starsId = data.stars;
            for (int i = 0; i < stars.transform.childCount; i++)
                if (starsId[i])
                    stars.transform.GetChild(i).gameObject.SetActive(false);

        }
        // If there's no checkpoint (it's the beginning of the level)
        else
        {
            Points = 0;
            // Save the current position
            SaveSystem.SaveCheckpointData(player.transform.position, -1, egg.transform.position);
        }
    }

    public void UpdatePointsText()
    {
        pointsText.text = Points.ToString() + " stars";
    }

    public void UpdateInstructionsText(string instructions)
    {
        instructionsText.text = instructions;
    }

    public void UpdateTimeText(string time)
    {
        timeText.text = time;
    }
}
