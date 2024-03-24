using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2.0f;
    [SerializeField]
    private float sprintSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private float currentSpeed;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool sprinting = false;

    private bool canFire = true;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        sprinting = context.action.triggered;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && canFire)
        {
            StartCoroutine(FireWithDelay());
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float cameraYRotation = Camera.main.transform.rotation.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0f, cameraYRotation, 0f);

        Vector3 moveDirection = rotation * new Vector3(movementInput.x, 0f, movementInput.y);

        controller.Move(moveDirection.normalized * Time.deltaTime * currentSpeed);

        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (sprinting)
        {
            currentSpeed = sprintSpeed;
            SettingsManager.instance.playerFOV = 65f;
        }
        else
        {
            currentSpeed = walkSpeed;
            SettingsManager.instance.playerFOV = 60f;
        }

    }
    IEnumerator FireWithDelay()
    {
        canFire = false;

        yield return new WaitForSeconds(0.2f);

        Fire();

        canFire = true;
    }
    private void Fire()
    {
        Debug.Log("fired");
    }
}
