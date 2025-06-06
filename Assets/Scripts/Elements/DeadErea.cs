using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadErea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats stats = collision.GetComponent<CharacterStats>();

        if (stats != null)
        {
            stats.Falling();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
