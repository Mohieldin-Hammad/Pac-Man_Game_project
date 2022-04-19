using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the four nodes that will be for 
    public GameObject upRightGateNode;
    public GameObject downRightGateNode;
    public GameObject upLeftGateNode;
    public GameObject downLeftGateNode;


    public AudioSource sound;


    void Awake()
    { 
        // playing music
        sound = GetComponent<AudioSource>();
        if (sound.mute == true) sound.mute = false; 
        sound.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
