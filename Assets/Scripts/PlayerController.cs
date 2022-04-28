using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the user play mode
public class PlayerController : MonoBehaviour
{
    MovementController movementC;
    public Vector2 theDirection;


    // Start is called before the first frame update
    void Awake()
    {
        // make a reference to the movement controller
        movementC = GetComponent<MovementController>();
    }




    // Update is called once per frame
    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementC.SetDirection("up");  
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementC.SetDirection("down");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementC.SetDirection("left");

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementC.SetDirection("right");
        }

    }
}
