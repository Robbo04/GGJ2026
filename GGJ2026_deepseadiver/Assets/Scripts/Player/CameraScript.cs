using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public float mouseSensitivity = 50f;
    public Transform playerBody;
    //public Transform helmetBody;
    public Transform playerHelmet;
    private bool isCameraLock = false; 

    float xRotation = 0f;
    float yRotation = 0f;
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

    public void LockOnToggle()
    {
        isCameraLock = !isCameraLock;
        if (isCameraLock)
        {
            //camera look at main open (lerp to).
        }
    }

    void Update()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -40f, 40f);

        if (isCameraLock)
        {
            //if playing is holding control.
            //y rotation
            transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
            //x rotation
        }
        else
        {
            //if camera is not holding control
            //y rotation
            transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            //x rotation
            playerHelmet.Rotate(Vector3.up * mouseX);
            playerBody.Rotate(Vector3.up * mouseX);
        }        
    }
}
