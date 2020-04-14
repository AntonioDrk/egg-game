using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Points = 0;
        UpdatePointsText();
        UpdateInstructionsText("");
    }

    public void UpdatePointsText()
    {
        pointsText.text = Points.ToString() + " stars";
    }

    public void UpdateInstructionsText(string instructions)
    {
        instructionsText.text = instructions;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
