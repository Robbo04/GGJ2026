using UnityEngine;
using UnityEngine.InputSystem;

public class HelmetMovement : MonoBehaviour
{
   //public float helmetSensitivity = 50f;
   //public Transform helmetBody;
   //private PlayerInputActions helmetControls;
   //private Vector2 lookInput;
   //
   //void Awake()
   //{
   //    helmetControls = new PlayerInputActions();
   //}
   //
   //void OnEnable()
   //{
   //    helmetControls.PlayerController.Enable();
   //    helmetControls.PlayerController.Look.Performed += OnLook;
   //    helmetControls.PlayerController.Look.canceled += OnLook;
   //}
   //
   //void OnDisable()
   //{
   //    helmetControls.PlayerController.performed -= OnLook;
   //    helmetControls.PlayerController.canceled -= OnLook;
   //    helmetControls.PlayerController.Disable();
   //}
   //
   //private void OnLook(InputAction.CallbackContext context)
   //{
   //    lookInput = context.ReadValue<Vector2>();
   //}
   //
   //void Start()
   //{
   //    Cursor.lockState - CursorLockMode.Locked;
   //}
   //// Update is called once per frame
   //void Update()
   //{
   //    float mouseX = lookInput.x * helmetSensitivity * Time.deltaTime;
   //
   //    playerBody.Rotate(Vector3.up * mouseX);
   //    print(mouseX);
   //}
}
