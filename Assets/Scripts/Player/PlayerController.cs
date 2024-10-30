using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovement;
    public float jumpForce;
    public LayerMask groundLayerMask;
    public float useStamina;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private Rigidbody rigidbody;
    //private Interaction interact;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        //interact = GetComponent<Interaction>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        cameraLook();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovement = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovement = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {          
            
                StartCoroutine(UseStamina());
                moveSpeed *= 1.5f;
              
        }
        else if( context.phase == InputActionPhase.Canceled)
        {
            moveSpeed /= 1.5f;
        }
    }

    IEnumerator UseStamina()
    {
        CharacterManager.Instance.Player.condition.UseStamina(useStamina);
        yield return new WaitForSecondsRealtime(0.5f);
    }


    private void cameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot,0,0);

        transform.localEulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void Move()
    {
        Vector3 direction = transform.forward * curMovement.y + transform.right * curMovement.x;
        direction *= moveSpeed;
        direction.y = rigidbody.velocity.y;

        rigidbody.velocity = direction;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
             new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
              new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down),
               new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down)
        };
        
        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 2.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    //public void Input(InputAction.CallbackContext context) 리팩토링 예제
    //{
        //interact.OnInteractInput(context);
    //}
}