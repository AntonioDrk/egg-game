using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private bool _killPlayer;

    [SerializeField]
    private bool activated = true;

    private TilemapRenderer tilemapRenderer;

    private void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
            return;

        if (_killPlayer && other.gameObject.CompareTag("Player"))
        {
            // player died from falling
            if(this.gameObject.name == "DeathLine")
            {
                SaveSystem.SaveStatsData(0, 0, 1, 0);
            }
            else
                SaveSystem.SaveStatsData(0, 1, 0, 0);

            other.GetComponent<PlayerContoller>().KillPlayer();
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            other.GetComponent<EggController>().Explosion();
        }
    }

    public void OnInteract(bool isInteracted)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.laser);
        activated = isInteracted;
        tilemapRenderer.enabled = !tilemapRenderer.enabled;
    }
}
