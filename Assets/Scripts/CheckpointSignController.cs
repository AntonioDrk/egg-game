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
                    SaveSystem.SaveCheckpointData(this.gameObject.transform.position, id);
                }
            }
            else
            {
                Debug.Log("Save data for level " + (SceneManager.GetActiveScene().buildIndex - 2));
                SaveSystem.SaveLevelData(SceneManager.GetActiveScene().buildIndex - 2, GameManager.Instance.Points);
                UIManager.Instance.LoadScene(0);
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
