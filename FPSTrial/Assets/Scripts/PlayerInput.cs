//Amorina Tabera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
{
    //KAVAN**************************************************************************************************************************************************
    //YOUR CALLS ARE IN THE METHOD OnPause
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    //*********************************************************************************************************************************************************
    
    public float speed;                     //Speed of Player
    public PlayerControls controls;         //Gets Input System Controls from script called PlayerControls
    public Vector2 motion;                  //Vector for movement of Player
    public Vector2 mouseInput;              //Vector for looking around for Player

    //SerializedField makes this variable visible in the unity editor while still maintaining privacy, need to set for other vairables
    [SerializeField] private CharacterController controller = null; //Get Character Controller of Player, apparently better than rigidbody when not wanting to deal with physics

    public float sensitivity;               //Mouse sensitivity
    public float MaxLookUpAngle;            //How high Player can look
    public float MinLookUpAngle;            //How low Player can look

    public float rotX = 0, rotY = 0;        //Rotations for camera

    public Transform ffCamera;              //To hold the FFCamera

    public Gun gun;                         //Get Gun script

    Vector3 movement;                       //Vector to hold movment

    public float health;                    //To hold current health
    public float maxHealth;                 //To hold max health

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();   //Get the Character Controller component of the player
        Cursor.lockState = CursorLockMode.Locked;           //Keep cursor in the center, not wandering
        Cursor.visible = false;                             //Make cursor invisible 
        maxHealth = health;                                 //Set max health to health
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
            gun.MyInput();                                         //Call method Shoot from script Gun
    }

    //Inputsystem.inputActions.Player.Quit.triggered
    public void OnPause(InputAction.CallbackContext context)
    {
        //Application.Quit();                                         // Quit the game

        //KAVAN******************************************************************************************************KAVAN********************************************************
        //THIS IS PUT IN WHERE IT CHECKS IF PLAYER PRESSED THE ESC BUTTON, YOUR METHODS ARE AT THE BOTTOM
        if (GameIsPaused)
            Resume();
        else
            Pause();
        //***************************************************************************************************************************************************************************
    }

    void Update()
    {
        Move();         //Calls method Move()
        Turn();         //Calls method Turn()
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

        ffCamera.transform.localRotation = Quaternion.Euler(-rotY, mouseInput.x, 0f);   //Move camera with mouse
        transform.Rotate(Vector3.up * mouseInput.x);                                    //Move Player with camera
    }


    //Method so Player can take damage
    public void TakeDamage(int damage)
    {
        health -= damage;                   //decrement health according to damage taken

        if (health <= 0)                    //If player dies
        {
            Debug.Log("YOU DIED!");         //Put actual death here
        }
    }

    //KAVAN*****************************************************************************************************************************************************************
    //YOUR METHODS FOR PAUSE MENU ARE HERE
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    //************************************************************************************************************************************************************************
}