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

    private void Awake()
    {
        Instance = this;
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

        CheckpointData data = SaveSystem.LoadCheckpointData();
        // If you found the checkpoint data file
        if (data != null)
        {
            Points = data.points;
            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        // If there's no checkpoint (it's the beginning of the level)
        else
        {
            Points = 0;
            // Save the current position
            SaveSystem.SaveCheckpointData(player.transform.position);
            // Save the current level
            SaveSystem.SaveLevelData();
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
}
