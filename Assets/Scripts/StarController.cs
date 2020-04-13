using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Points++;
            Instantiate(Resources.Load<GameObject>("StarParticles") as GameObject, transform.position, Quaternion.identity);
            Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
}
