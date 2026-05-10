using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 1f;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Transform endingPosition;

    private Rigidbody platformRigidbody;
    private CharacterController playerController;

    private void Awake()
    {
        platformRigidbody = GetComponent<Rigidbody>() ?? GetComponentInParent<Rigidbody>();

        if (platformRigidbody == null)
        {
            Debug.LogError("MovingPlatform requires a Rigidbody on this object or a parent object.", this);
        }
    }

    private void FixedUpdate()
    {
        if (platformRigidbody == null)
        {
            return;
        }

        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 currentPosition = platformRigidbody.position;
        Vector3 targetPosition = GetTargetPosition();
        Vector3 movementDelta = targetPosition - currentPosition;

        platformRigidbody.MovePosition(targetPosition);
        MovePlayerWithPlatform(movementDelta);
    }

    private Vector3 GetTargetPosition()
    {
        float interpolation = Mathf.PingPong(Time.time * platformSpeed, 1f);
        return Vector3.Lerp(startingPosition.position, endingPosition.position, interpolation);
    }

    private void MovePlayerWithPlatform(Vector3 movementDelta)
    {
        if (playerController == null || movementDelta == Vector3.zero)
        {
            return;
        }

        playerController.Move(movementDelta);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerController = other.GetComponentInParent<CharacterController>();

        if (playerController == null)
        {
            Debug.LogWarning("Player entered moving platform trigger but has no CharacterController.", this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerController = null;
    }
}
