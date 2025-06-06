using System.Collections;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField] private int areaSoundIndex;

    private Coroutine stopSFXGradually;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (areaSoundIndex < 0 || areaSoundIndex >= AudioManager.instance.sfx.Length)
                return;

            if (stopSFXGradually != null)
            {
                StopCoroutine(stopSFXGradually);
            }

            AudioManager.instance.PlayAreaLoopSFX(areaSoundIndex); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (collision.GetComponent<Player>() != null)
        {
            if (areaSoundIndex < 0 || areaSoundIndex >= AudioManager.instance.sfx.Length)
                return;

            stopSFXGradually = StartCoroutine(
                AudioManager.instance.DecreaseVolume(AudioManager.instance.sfx[areaSoundIndex])
            );
        }
    }
}
