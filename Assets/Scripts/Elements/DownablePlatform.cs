using UnityEngine;
using System.Collections;

public class DownablePlatform : MonoBehaviour
{
    private Collider2D platformCollider;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    public void TemporarilyDisableCollider(float duration)
    {
        StartCoroutine(DisableColliderCoroutine(duration));
    }

    private IEnumerator DisableColliderCoroutine(float duration)
    {
        platformCollider.enabled = false;
        yield return new WaitForSeconds(duration);
        platformCollider.enabled = true;
    }
}

