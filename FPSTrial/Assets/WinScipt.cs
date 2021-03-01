using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScipt : MonoBehaviour
{
    
        //win stuff
    public GameObject winScreen;
    public GameObject gameScreen;

    public AudioSource sound;

    // Update is called once per frame
    void OnTriggerEnter()
    {
     
        winScreen.SetActive(true);
        gameScreen.SetActive(false);
        Time.timeScale = 0f;
        sound.mute = !sound.mute;
        
    }
}
