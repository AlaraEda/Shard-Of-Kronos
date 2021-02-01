using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public Vector2 inputView;

    public float mouseSensitivity = 100f;
    
    private Vector2 Look;
    private float xRotation;

    private Transform playerBody;

    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        playerBody = transform.parent;
        //controls
        playerInputActions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
        
        //cam
        playerInputActions.Movement.View.performed += x => inputView = x.ReadValue<Vector2>();
  
        // playerInputActions.Actions.Jump.performed += x => Jump();
    }

    #region Enable/Disable

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }


    #endregion

    private void FixedUpdate()
    {
        var MouseX = inputView.x * mouseSensitivity * Time.deltaTime;
        var MouseY = inputView.y * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate((Vector3.up * MouseX));
        
    }
}
