using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("INPUT")]
    [SerializeField] private InputAction Movement;
    [SerializeField] private InputAction Look;
    [SerializeField] private InputAction Gun;
    [SerializeField] private InputAction Quit;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private Camera cam = null;

    [Header("STATS")]
    [SerializeField] public float speed = 3.5f;
    [SerializeField] public float mousesSens = 100f;

    public float camRotation = 0;
    public Gun gun;                         //Get Gun script

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Shooting();
        Quitting();
    }

    private void Move()
    {
        float x = Movement.ReadValue<Vector2>().x;
        float z = Movement.ReadValue<Vector2>().y;
        //reconverts the axis to work on the XZ plane
        Vector3 direction = transform.right * x + transform.forward * z;
        //print ("pog");
        controller.Move(direction * Time.deltaTime * speed);
    }

    private void Turn()
    {
        float mouseX = Look.ReadValue<Vector2>().x * mousesSens * Time.deltaTime;
        float mouseY = Look.ReadValue<Vector2>().y * mousesSens * Time.deltaTime;

        camRotation -= mouseY;
        camRotation = Mathf.Clamp(camRotation, -90, 90); //clamps mouse
        cam.transform.localRotation = Quaternion.Euler(camRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Shooting()
    {
        gun.Shoot();
    }

    private void Quitting()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        Movement.Enable();
        Look.Enable();
        Gun.Enable();
        Quit.Enable();
    }

    private void OnDisable()
    {
        Movement.Disable();
        Look.Disable();
        Gun.Enable();
        Quit.Enable();
    }
}