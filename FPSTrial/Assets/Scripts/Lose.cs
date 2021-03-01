using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    //win stuff
    public GameObject loseScreen;
    public GameObject gameScreen;

    public AudioSource sound;

    // Update is called once per frame
    void OnTriggerEnter()
    {
        //Amorina**************************************************************************
        Cursor.lockState = CursorLockMode.None;           //Unlock cursor
        Cursor.visible = true;                             //Make cursor visible 
        //*********************************************************************************

        loseScreen.SetActive(true);
        gameScreen.SetActive(false);
        Time.timeScale = 0f;
        sound.mute = !sound.mute;

    }
}
