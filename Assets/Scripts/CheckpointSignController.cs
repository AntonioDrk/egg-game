using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointSignController : MonoBehaviour
{
    public int id;
    public bool lastCheckpoint; // true if it's the end of the level

    public GameObject finish;
    public Text timer;

    void Start()
    {
        if(lastCheckpoint)
        {
            finish = GameObject.Find("Finish");
            timer = GameObject.Find("Finish/Panel/Time/Timer").GetComponent<Text>();

            var currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel - 2 == 3)
            {
                var nextLevelButton = GameObject.Find("Finish/Panel/Next Level Button");
                nextLevelButton.SetActive(false);
            }

            finish.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!lastCheckpoint)
            {
                CheckpointData data = SaveSystem.LoadCheckpointData();

                if (id > data.id)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.checkpoint);
                    GameManager.Instance.UpdateInstructionsText("Progress saved");

                    var egg = GameObject.Find("Egg");
                    Vector3 eggPos = egg.transform.position;
                    
                    // if the egg is in hand's player, it should teleport on the ground next time
                    if(egg.GetComponent<EggController>().isInHand)
                    {
                        eggPos = new Vector3(other.transform.position.x, other.transform.position.y - 0.48f, 0f);
                    }

                    SaveSystem.SaveCheckpointData(this.gameObject.transform.position, id, eggPos);
                }
            }
            // it's the flag
            else
            {
                var egg = GameObject.Find("Egg");
                if(egg.GetComponent<EggController>().isInHand)
                {
                    finish.SetActive(true);
                    var time = TimerController.Instance.GetTime();
                    timer.text = TimerController.Instance.GetTimeString(time);
                    var stars = GameManager.Instance.Points;
                    GameManager.Instance.FinishLevel();

                    for (int j = 1; j <= stars; j++)
                    {
                        var star = GameObject.Find("Star" + j.ToString());
                        star.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    }

                    Debug.Log("Save the data for level " + (SceneManager.GetActiveScene().buildIndex - 2));
                    SaveSystem.SaveLevelData(SceneManager.GetActiveScene().buildIndex - 2, stars);
                    SaveSystem.SaveStatsData(0, 0, 0, time);
                }
                else
                    GameManager.Instance.UpdateInstructionsText("Where is the egg?");
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UpdateInstructionsText("");
        }
    }
}
