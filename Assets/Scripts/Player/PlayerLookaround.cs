using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookaround : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform player;

    private float upDownRotation;

    void Start()
    {
        LockCursorInsideScene();
    }

    void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        float limitOfDownRotation = -90f;
        float limitOfUpRotation = 90f;

        float yValueDuringUpDownRotation = 0f;
        float zValueDuringUpDownRotation = 0f;

        SetUpDownRotation(Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);

        LimitUpDownRotation(limitOfDownRotation, limitOfUpRotation);

        ApplyRotation(yValueDuringUpDownRotation, zValueDuringUpDownRotation);
    }

    private void LockCursorInsideScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LimitUpDownRotation(float limitOfDownRotation, float limitOfUpRotation)
    {
        upDownRotation = Mathf.Clamp(upDownRotation, limitOfDownRotation, limitOfUpRotation);
    }

    private void ApplyRotation(float yValueDuringUpDownRotation, float zValueDuringUpDownRotation)
    {
        float leftRightRotationThisFrame = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(upDownRotation, yValueDuringUpDownRotation, zValueDuringUpDownRotation);

        player.Rotate(Vector3.up * leftRightRotationThisFrame);
    }

    public float GetMouseSensitivity()
    {
        return this.mouseSensitivity;
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        this.mouseSensitivity = mouseSensitivity;
    }

    public Transform GetPlayer()
    {
        return this.player;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    public float GetUpDownRotation()
    {
        return this.upDownRotation;
    }

    public void SetUpDownRotation(float upDownRotation)
    {
        this.upDownRotation -= upDownRotation;
    }
}
