using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointSignController : MonoBehaviour
{
    public int id;
    public bool lastCheckpoint; // true if it's the end of the level

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
                    Debug.Log("Save data for level " + (SceneManager.GetActiveScene().buildIndex - 2));
                    SaveSystem.SaveLevelData(SceneManager.GetActiveScene().buildIndex - 2, GameManager.Instance.Points);
                    SaveSystem.SaveStatsData(0, 0, 0, TimerController.Instance.GetTime());
                    UIManager.Instance.LoadScene(0);
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
