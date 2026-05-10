using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 1f;
    [SerializeField] private float respawnDelay = 3f;

    [SerializeField] private GameObject disappearingPlatform;

    private Renderer[] platformRenderers;
    private Collider[] platformColliders;
    private bool isDisappearing;

    private void Awake()
    {
        CachePlatformComponents();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanDisappear(other))
        {
            return;
        }

        StartCoroutine(DisappearAndRespawn());
    }

    private IEnumerator DisappearAndRespawn()
    {
        isDisappearing = true;

        yield return new WaitForSeconds(disappearDelay);
        SetPlatformEnabled(false);

        yield return new WaitForSeconds(respawnDelay);
        SetPlatformEnabled(true);

        isDisappearing = false;
    }

    private void CachePlatformComponents()
    {
        if (disappearingPlatform == null)
        {
            disappearingPlatform = gameObject;
        }

        platformRenderers = disappearingPlatform.GetComponentsInChildren<Renderer>();
        platformColliders = disappearingPlatform.GetComponentsInChildren<Collider>();
    }

    private bool CanDisappear(Collider other)
    {
        return other.CompareTag("Player") && !isDisappearing;
    }

    private void SetPlatformEnabled(bool isEnabled)
    {
        foreach (Renderer platformRenderer in platformRenderers)
        {
            platformRenderer.enabled = isEnabled;
        }

        foreach (Collider platformCollider in platformColliders)
        {
            platformCollider.enabled = isEnabled;
        }
    }
}
