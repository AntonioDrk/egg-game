﻿using UnityEngine;

public class CheckpointSignController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UpdateInstructionsText("Progress saved");
            SaveSystem.SaveCheckpointData(this.gameObject.transform.position);
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
