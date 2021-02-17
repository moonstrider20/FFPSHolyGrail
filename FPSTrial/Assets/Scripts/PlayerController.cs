//Amorina Tabera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    public float speed;                     //Speed of Player
    public PlayerControls controls;         //Gets Input System Controls from script called PlayerControls
    public Vector2 motion, mouseInput;      //vector for movement of player(motion), mouse input

    private Rigidbody rb;                   //rigidbody of the Player

    public float sensitivity;              //Mouse sensitivity
    public float MaxLookUpAngle;            //How high Player can look
    public float MinLookUpAngle;            //How low Player can look

    public float rotX = 0, rotY = 0;        //Rotations for camera

    public Transform MainCamera;            //To hold the FFCamera

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     //Get the rigidbody component of the player
        Cursor.lockState = CursorLockMode.Locked;   //Keep cursor in the center, not wandering
    }

    //To enable the Input System contorls for "Player"
    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    //Inputsystem.inputActioins.Player.Move.triggered
    public void OnMove(InputAction.CallbackContext context)
    {
        motion = context.ReadValue<Vector2>();                      //Get the values of the directions Player is moving (arrows and WASD)
    }

    //Inputsystem.inputActions.Player.Look.triggered
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();                  //read values of the mouse motion
    }

    // FixedUpdate is called every physisc step
    void FixedUpdate()
    {
        //moves player
        Vector3 movement = transform.right * motion.x + transform.forward * motion.y; // GLOBAL[new Vector3(motion.x, 0.0f, motion.y);] <- that but relative to the direction of the camera
        rb.AddForce(movement * speed);                                                //Move the player by adding force
    }

    // Update is called once per frame
    void Update()
    {
        //rotX += mouseInput.x * sensitivity;// * Time.deltaTime;                        //adjust where camera will look according to x...which don't actually need for the moment
        rotY += mouseInput.y * sensitivity;// * Time.deltaTime;                        //adjust where camera will look according to y
        rotY = Mathf.Clamp(rotY, MinLookUpAngle, MaxLookUpAngle);                       //Clamp how far camera can look up and down

        //try this...
        MainCamera.transform.localRotation = Quaternion.Euler(-rotY, mouseInput.x, 0f);     //Move direction camera faces, must use localRotation since camera child of player object
        rb.transform.Rotate(Vector3.up * mouseInput.x);                                 //Move the player to face direction player puts camera in
    }
}