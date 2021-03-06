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

    // Is referenced to the palletExploration of node in position (-0.5, -8.5)
    public PalletExploration palletExploration;

    // pacman node is the start node to start the search from
    public GameObject pacmanNode;
    
    // store the first pacmanNode for reseting 
    public GameObject originalPacmanNode { get; private set; }

    // original features ... will be used for reseting the pacman to the the first orignal setting
    public Vector3 originalPacmanPosition { get; private set; }
    public Quaternion originalPacmanRotation { get; private set; }

    // all pallets in the game are stored in foods list
    List<GameObject> foods;
    public int foodsCount { get; private set; }

    // will be used for starting the game
    public static bool reStartGame;

    // reference for Player gameObject
    public GameObject pacman;

    // the score of pacman
    public int score;
    public Text scoreText;

    public AudioSource sound;

    // Adding Scene control class for managing the view of the game
    public SceneControl scene;

    // will apply the mode of GameSpecification.gameMode
    private string mode;

    private float timeLeft;
    public Text timeText;

    void Awake()
    {

        // get reference to the scene controller
        scene = GetComponent<SceneControl>();

        // playing music
        sound = GetComponent<AudioSource>();
        if (sound.mute == true) sound.mute = false; 
        sound.Play();

        // set score to 0
        score = 0;

        // store the original node
        originalPacmanNode = pacmanNode;

        // store the first pacman state
        originalPacmanPosition = pacmanNode.transform.position;
        originalPacmanRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        // this will allow the AIPlayerController to findout if the it's a new game and the pacman 
        // show restart or not. If it's a new game the restartGame will will be true and the AIPlayerController will 
        // return it back to false after executing the ReStart() method
        reStartGame = false;


        if (pacman.activeSelf)
        {
            pacman.SetActive(false);
        }
    }


    private void Start()
    {
        // timer time
        timeLeft = 90f;


        string mode = GameSpecification.GameMode;
        if (mode == "USER")
        {
            pacman.GetComponent<PlayerController>().enabled = true;
            pacman.GetComponent<MovementController>().enabled = true;
            pacman.GetComponent<AIPlayerController>().enabled = false;
        }
        else if (mode == "AI") {
            pacman.GetComponent<PlayerController>().enabled = false;
            pacman.GetComponent<MovementController>().enabled = false;
            pacman.GetComponent<AIPlayerController>().enabled = true;
        }

        if (!pacman.activeSelf)
        {
            pacman.SetActive(true);
        }

        // using the BFS to store all pallets in foods list
        foods = palletExploration.BFS_Exploration();
        foodsCount = foods.Count;
        
        // reseting all the game
        resetState();

        // and start new one
        newGame();
    }



    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            updateTimer(timeLeft);
        }
        else
        {
            scene.LoadResultScene("LOST");   
        }
    }


    void updateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }



    // activate the pacman and reStartGame will let AIPlayerController restart the game 
    // and get all the pallets list again
    private void newGame()
    {
        pacman.SetActive(true);
        setScore(0);
        reStartGame = true;
    }



    // take the amount of score and add it to the score variable ... this method also shaow the score on the screen
    public void setScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score.ToString();

        if (score == foodsCount)
        {
            pacman.SetActive(false);
            //resetState();


            scene.LoadResultScene("WIN");
        }
    }



    // Reset every thing in the game to it's default state 
    private void resetState()
    {
        resetPallet();
        resetPacman();
        // to let pacman get the list of all the nodes to start the game again from beginning
        pacman.GetComponent<AIPlayerController>().ReStart();
        
        score = 0;
        scoreText.text = "Score: ";
    }



    // Reset all the pallet to it's default and render them on the board
    private void resetPallet()
    {
        PalletController palletC;
        List<GameObject> allPallets = new List<GameObject>(foods);
        foreach(GameObject thePallet in allPallets)
        {
            palletC = thePallet.GetComponent<PalletController>();

            if (!palletC.hasPallet)
            {
                palletC.hasPallet = true;  
                palletC.pallet.enabled = true;
            }
        }
    }



    // reset the pacman to the default position
    private void resetPacman()
    {
        AIPlayerController pacmanAiController = pacman.GetComponent<AIPlayerController>();
        pacman.transform.position = originalPacmanPosition;
        pacman.transform.rotation = originalPacmanRotation;
        
        //if (!pacman.activeSelf)
        //{
        //    pacman.SetActive(true);
        //}
    }



    // get all the pallets of the game
    public List<GameObject> getFoods()
    {
        return new List<GameObject>(foods);
    }



    // the originalPacmanNode is the node under the pacman at the pacman default position and this node will 
    // be used in AIPlayerController in the Restart method to Restart the game
    public GameObject getOriginalNode()
    {
        return originalPacmanNode;
    }
}
