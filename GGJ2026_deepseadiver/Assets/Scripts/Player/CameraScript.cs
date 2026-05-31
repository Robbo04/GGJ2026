using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public float mouseSensitivity = 50f;
    public Transform playerBody;
    //public Transform helmetBody;
    public Transform playerHelmet;
    private bool isCameraLock = false;
    private bool currentlyLocked = false;
    private bool isReturningToCenter = false;

    //look input rotation floats
    float xRotation = 0f;
    float yRotation = 0f;
    //rotation input floats
    float currentXRotation = 0f;
    float currentYRotation = 0f;

    private PlayerInputActions playerControls;
    private Vector2 lookInput;

    void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerControls.PlayerController.Enable();
        //look controls enabled.
        playerControls.PlayerController.Look.performed += OnLook;
        playerControls.PlayerController.Look.canceled += OnLook;
        //freelook controls enabled.
        playerControls.PlayerController.Freelook.performed += Freelook;
        playerControls.PlayerController.Freelook.canceled += Freelook;
    }

    void OnDisable()
    {
        //look controls disabled.
        playerControls.PlayerController.Look.performed -= OnLook;
        playerControls.PlayerController.Look.canceled -= OnLook;
        //freelook controls disabled.
        playerControls.PlayerController.Freelook.performed -= Freelook;
        playerControls.PlayerController.Freelook.canceled -= Freelook;
        playerControls.PlayerController.Disable();
    }

    private void OnLook(InputAction.CallbackContext Lookcontext)
    {
        lookInput = Lookcontext.ReadValue<Vector2>();
    }

    private void Freelook(InputAction.CallbackContext FreecamContext)
    {
        print(isCameraLock);
        if (FreecamContext.performed)
        {
            isCameraLock = true;          
        }
        else
        {
            isCameraLock = false;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //public void LockOnToggle()
    //{
    //    //will trigger when player releases control after locking camera
    //    print("Return");
    //    print(transform.localRotation.eulerAngles);
    //}

    void Update()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -80f, 40f);

        if (isCameraLock)
        {
            //if player is holding control.
            //y rotation
            transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
            //x rotation
            currentlyLocked = true;
            isReturningToCenter = false; // Reset return flag while actively locked
        }
        else
        {
            //if player is not holding control
            if (currentlyLocked)
            {
                // First frame after releasing lock - capture current rotation
                if (!isReturningToCenter)
                {
                    Vector3 currentEuler = transform.localRotation.eulerAngles;
                    currentXRotation = currentEuler.y;
                    currentYRotation = currentEuler.x;
                    // Normalize angles to -180 to 180 range
                    if (currentXRotation > 180f) currentXRotation -= 360f;
                    if (currentYRotation > 180f) currentYRotation -= 360f;
                    isReturningToCenter = true;
                }
                
                // Smoothly interpolate camera back to center over multiple frames
                float returnSpeed = 200f * Time.deltaTime; // Adjust speed as needed
                
                currentXRotation = Mathf.MoveTowards(currentXRotation, 0f, returnSpeed);
                currentYRotation = Mathf.MoveTowards(currentYRotation, 0f, returnSpeed);
                
                // Apply the interpolated rotation
                transform.localRotation = Quaternion.Euler(currentYRotation, currentXRotation, 0f);
                
                // Check if we're close enough to center to consider it done
                if (Mathf.Abs(currentXRotation) < 0.1f && Mathf.Abs(currentYRotation) < 0.1f)
                {
                    currentXRotation = 0f;
                    currentYRotation = 0f;
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    currentlyLocked = false;
                    isReturningToCenter = false;
                    print("Camera returned to center");
                }
            }
            else
            {
                //Lets player control once camera has returned to centre point again.
                //y rotation
                transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
                //x rotation
                playerHelmet.Rotate(Vector3.up * mouseX);
                playerBody.Rotate(Vector3.up * mouseX);
            }
               
        }        
    }
}
