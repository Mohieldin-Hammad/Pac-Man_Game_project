using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // the four nodes that will be for 
    public GameObject upRightGateNode;
    public GameObject downRightGateNode;
    public GameObject upLeftGateNode;
    public GameObject downLeftGateNode;

    //public PalletExploration palletExploration;
    //public GameObject pacman;
    //public int palletsCount;

    private int score;

    public Text scoreText;


    public AudioSource sound;


    void Awake()
    { 
        // playing music
        sound = GetComponent<AudioSource>();
        if (sound.mute == true) sound.mute = false; 
        sound.Play();
        
        score = 0;

        // palletsCount = palletExploration.palletsCount();
        //Debug.Log(palletsCount);
    }



    private void Start()
    {
        newGame();
    }


    private void newGame()
    {
        setScore(0);
    }


    public void setScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score.ToString();
        
    }

    private void resetState()
    {

    }

}
