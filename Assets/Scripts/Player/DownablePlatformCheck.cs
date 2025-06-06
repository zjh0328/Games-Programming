using UnityEngine;

public class DownablePlatformCheck : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = PlayerManager.instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DownablePlatform platform = collision.GetComponent<DownablePlatform>();

        if (platform != null)
        {
            player.lastPlatform = platform;
            player.isOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DownablePlatform platform = collision.GetComponent<DownablePlatform>();

        if (platform != null && player.lastPlatform == platform)
        {
            player.lastPlatform = null;
            player.isOnPlatform = false;
        }
    }
}

