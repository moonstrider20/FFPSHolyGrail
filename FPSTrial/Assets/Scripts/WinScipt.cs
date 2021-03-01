using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScipt : MonoBehaviour
{
    //Amorina*************************************************************************
    //public PlayerInput player;
    //********************************************************************************
        //win stuff
    public GameObject winScreen;
    public GameObject gameScreen;

    public AudioSource sound;

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Amorina**************************************************************************
            Cursor.lockState = CursorLockMode.None;           //Unlock cursor
            Cursor.visible = true;                             //Make cursor visible 
            //*********************************************************************************

            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            Time.timeScale = 0f;
            sound.mute = !sound.mute;
        }
        
    }
}
