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
            other.GetComponent<PlayerContoller>().KillPlayer();
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            other.GetComponent<EggController>().Explosion();
        }
    }

    public void OnInteract(bool isInteracted)
    {
        activated = isInteracted;
        tilemapRenderer.enabled = !tilemapRenderer.enabled;
    }
}
