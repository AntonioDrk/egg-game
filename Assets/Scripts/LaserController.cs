using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private bool _killPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_killPlayer && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerContoller>().KillPlayer();
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            other.GetComponent<EggController>().Explosion();
        }
    }
}
