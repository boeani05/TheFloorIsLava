using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;

    public float movementSpeed;
    public float jumpPower;
    public float gravity;

    private Vector3 velocityOfPlayer;
    private bool isPlayerGrounded;

    public Transform footSensor;
    public float distanceFromGround;
    public LayerMask groundMask;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isPlayerGrounded = DoesPlayerTouchGround();

        if (isPlayerGrounded && DoesNotMoveVertically())
        {
            velocityOfPlayer.y = PushPlayerSlightlyToGround();
        }

        PrepareEntireMovementOfPlayer();
        ApplyGravity();

        MovePlayer();
    }

    private bool DoesPlayerTouchGround()
    {
        return Physics.CheckSphere(footSensor.position, distanceFromGround, groundMask);
    }

    private bool DoesNotMoveVertically()
    {
        return velocityOfPlayer.y < 0;
    }

    private float SetInputAxis(String inputAxis)
    {
        return Input.GetAxis(inputAxis);
    }

    private float PushPlayerSlightlyToGround()
    {
        return -2f;
    }

    private void PrepareEntireMovementOfPlayer()
    {
        MovePlayerWithCharacterController();

        JumpIfButtonIsPressed();
    }

    private void ApplyGravity()
    {
        if (transform.parent == null)
        {
            velocityOfPlayer.y -= gravity * Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        characterController.Move(velocityOfPlayer * Time.deltaTime);
    }

    private Vector3 GetCurrentMovementOfPlayer()
    {
        float horizontalKeyboardInput = SetInputAxis("Horizontal");
        float depthKeyboardInput = SetInputAxis("Vertical");

        return transform.right * horizontalKeyboardInput + transform.forward * depthKeyboardInput;
    }

    private void MovePlayerWithCharacterController()
    {
        Vector3 movementOfPlayer = GetCurrentMovementOfPlayer();

        velocityOfPlayer.x = movementOfPlayer.x * movementSpeed;
        velocityOfPlayer.z = movementOfPlayer.z * movementSpeed;
    }

    private void JumpIfButtonIsPressed()
    {
        if (Input.GetButtonDown("Jump") && isPlayerGrounded)
        {
            // Formel: v = Wurzel(h * -2 * g)
            velocityOfPlayer.y = Mathf.Sqrt(jumpPower * 2f * gravity);
        }
    }

    public float GetMovementSpeed()
    {
        return this.movementSpeed;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public float GetJumpPower()
    {
        return this.jumpPower;
    }

    public void SetJumpPower(float jumpPower)
    {
        this.jumpPower = jumpPower;
    }

    public float GetGravity()
    {
        return this.gravity;
    }

    public void SetGravity(float gravity)
    {
        this.gravity = gravity;
    }

    public Vector3 GetVelocityOfPlayer()
    {
        return this.velocityOfPlayer;
    }

    public void SetVelocityOfPlayer(Vector3 velocityOfPlayer)
    {
        this.velocityOfPlayer = velocityOfPlayer;
    }

    public bool IsPlayerGrounded()
    {
        return this.isPlayerGrounded;
    }

    public void SetIsPlayerGrounded(bool isPlayerGrounded)
    {
        this.isPlayerGrounded = isPlayerGrounded;
    }

    public Transform GetFootSensor()
    {
        return this.footSensor;
    }

    public void SetFootSensor(Transform footSensor)
    {
        this.footSensor = footSensor;
    }

    public float GetDistanceFromGround()
    {
        return this.distanceFromGround;
    }

    public void SetDistanceFromGround(float distanceFromGround)
    {
        this.distanceFromGround = distanceFromGround;
    }

    public LayerMask GetGroundMask()
    {
        return this.groundMask;
    }

    public void SetGroundMask(LayerMask groundMask)
    {
        this.groundMask = groundMask;
    }
}
