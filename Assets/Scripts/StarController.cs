using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Increase the points number and display it 
            GameManager.Instance.Points++;
            GameManager.Instance.UpdatePointsText();

            // Instantiate the particles prefab from Resources
            Instantiate(Resources.Load<GameObject>("StarParticles") as GameObject, transform.position, Quaternion.identity);

            // Destroy the star
            Destroy(this.transform.parent.gameObject);
        }
    }
}
