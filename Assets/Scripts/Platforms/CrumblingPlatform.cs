using System.Collections;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] private float crumbleDelay = 1f;
    [SerializeField] private float crumbleDuration = 5f;
    [SerializeField] private float disappearDelay = 1f;
    [SerializeField] private float respawnDelay = 3f;

    [SerializeField] private GameObject crumblingPlatform;

    private Renderer[] platformRenderers;
    private Collider[] platformColliders;
    private bool isDisappearing;

    private Vector3 originalPositionOfPlatform;
    private Vector3 currentPositionOfPlatform;

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

        StartCoroutine(CrumbleAndRespawn());
    }

    private IEnumerator CrumbleAndRespawn()
    {
        isDisappearing = true;

        yield return new WaitForSeconds(crumbleDelay);

        yield return ApplyCrumbleEffect();

        yield return new WaitForSeconds(disappearDelay);

        SetPlatformEnabled(false);

        yield return new WaitForSeconds(respawnDelay);

        RespawnPlatform();

        SetPlatformEnabled(true);

        isDisappearing = false;
    }

    private IEnumerator ApplyCrumbleEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < crumbleDuration)
        {
            Vector3 randomCrumblePosition = CalculateRandomCrumblePosition();

            currentPositionOfPlatform = originalPositionOfPlatform + randomCrumblePosition;
            crumblingPlatform.transform.position = currentPositionOfPlatform;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        crumblingPlatform.transform.position = originalPositionOfPlatform;
    }

    private Vector3 CalculateRandomCrumblePosition()
    {
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        float randomZ = Random.Range(-0.1f, 0.1f);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void RespawnPlatform()
    {
        currentPositionOfPlatform = originalPositionOfPlatform;
        crumblingPlatform.transform.position = currentPositionOfPlatform;
    }

    private void CachePlatformComponents()
    {
        if (crumblingPlatform == null)
        {
            crumblingPlatform = gameObject;
        }

        originalPositionOfPlatform = crumblingPlatform.transform.position;
        currentPositionOfPlatform = originalPositionOfPlatform;

        platformRenderers = crumblingPlatform.GetComponentsInChildren<Renderer>();
        platformColliders = crumblingPlatform.GetComponentsInChildren<Collider>();
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
