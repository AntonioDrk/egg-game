using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.star);
            // Increase the points number and display it 
            GameManager.Instance.Points++;
            GameManager.Instance.UpdatePointsText();
            GameManager.Instance.starsId[id] = true;

            // Instantiate the particles prefab from Resources
            Instantiate(Resources.Load<GameObject>("StarParticles") as GameObject, transform.position, Quaternion.identity);

            // Destroy the star
            Destroy(this.transform.parent.gameObject);
        }
    }
}
