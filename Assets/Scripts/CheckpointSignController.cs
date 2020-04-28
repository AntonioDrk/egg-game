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
                    SaveSystem.SaveCheckpointData(this.gameObject.transform.position, id, egg.transform.position);
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
