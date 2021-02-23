//Amorina Tabera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
{
    public float speed;                     //Speed of Player
    public PlayerControls controls;         //Gets Input System Controls from script called PlayerControls
    public Vector2 motion;                  //Vector for movement of Player
    public Vector2 mouseInput;              //Vector for looking around for Player

    //private Rigidbody rb;                   //rigidbody of the Player
    [SerializeField] private CharacterController controller = null;

    public float sensitivity;              //Mouse sensitivity
    public float MaxLookUpAngle;            //How high Player can look
    public float MinLookUpAngle;            //How low Player can look

    public float rotX = 0, rotY = 0;        //Rotations for camera

    public Transform MainCamera;            //To hold the FFCamera

    public Gun gun;                         //Get Gun script

    Vector3 movement;
    bool button;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();     //Get the rigidbody component of the player
        controller = GetComponent<CharacterController>();   //Get the Character Controller component of the player
        Cursor.lockState = CursorLockMode.Locked;   //Keep cursor in the center, not wandering
        Cursor.visible = false;                     //Make cursor invisible 
    }

    //To enable the Input System contorls for "Player"
    public void OnEnable()
    {
        //safety thing
        if (controls == null)
        {
            controls = new PlayerControls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    //To disable the Input System controls for "Player"
    public void OnDisable()
    {
        controls.Player.Disable();
    }

    //Inputsystem.inputActioins.Player.Move.triggered
    public void OnMovement(InputAction.CallbackContext context)
    {
        motion = context.ReadValue<Vector2>();                      //Get the values of the directions Player is moving (arrows, WASD, ect)
    }

    //Inputsystem.inputActions.Player.Look.triggered
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();                  //read values of the mouse(Look) motion
    }

    //Inputsytem.inputActions.Player.Gun.triggered
    public void OnGun(InputAction.CallbackContext context)
    {
        button = context.ReadValueAsButton();                       //Redundant, trying to fix double press problem
        if (button)
        {
            //gun.Shoot();                                            //Call method Shoot from script Gun
            gun.MyInput();
        }
    }

    //Inputsystem.inputActions.Player.Quit.triggered
    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();                                         // Quit the game
    }

    void Update()
    {
        Move();
        Turn();
        //Shooting();
        //Quitting();

    }

    //Method to move (should be in OnMovement but didn't work for some reason
    private void Move()
    {
        movement = transform.right * motion.x + transform.forward * motion.y; // GLOBAL[new Vector3(motion.x, 0.0f, motion.y);] <- that but relative to the direction of the camera
        controller.Move(movement * Time.deltaTime * speed);                   //Move the Player
    }

    //Method to look around (should be in OnLook, but left here since the movement didn't work)
    private void Turn()
    {
        rotX += mouseInput.x * sensitivity * Time.deltaTime;                            //Get horizontal input
        rotY += mouseInput.y * sensitivity * Time.deltaTime;                            //Get vertical input
        rotY = Mathf.Clamp(rotY, MinLookUpAngle, MaxLookUpAngle);                       //Restrict how far up and down Player can look

        MainCamera.transform.localRotation = Quaternion.Euler(-rotY, mouseInput.x, 0f); //Move camera with mouse
        transform.Rotate(Vector3.up * mouseInput.x);                                    //Move Player with camera
    }
}