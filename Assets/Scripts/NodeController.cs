 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool canMoveUp { get; private set; }
    public bool canMoveDown { get; private set; }
    public bool canMoveRight { get; private set; }
    public bool canMoveLeft { get; private set; }

    public GameObject nodeRight;
    public GameObject nodeLeft;
    public GameObject nodeDown;
    public GameObject nodeUp;

    public Vector2 theDirection;

    public bool upRightGate = false;
    public bool downRightGate = false;
    public bool upLeftGate = false;
    public bool downLeftGate = false;

    public bool visited;


    private void Awake()
    {
        // creating an array of the for directions
        Vector2[] vector2s = { Vector2.up, -Vector2.up, Vector2.right, -Vector2.right };

        // looping through the four directions and cheacking if there is another node next to it
        for (int i = 0; i < vector2s.Length; i++)
        {
            // RaycastHit is creating a hidding line to detect if the current node is having another node nearby
            // the layer mask in the raycast is one to not detect the pacman at the start of the game because the 
            // two node in the right and left of pacman will the pacman rigidbody is node and that will lead a bug
            RaycastHit2D hitDirection = Physics2D.Raycast(transform.position, vector2s[i], 1.5f, 1);
            
            // so if it's a node it will make a reference to it with selecting the direction
            if (hitDirection && hitDirection.collider.name == "Node") 
            { 
                switch (i)
                {
                    case 0:
                        canMoveUp = true;
                        nodeUp = hitDirection.collider.gameObject;
                        break;
                    case 1:
                        canMoveDown = true;
                        nodeDown = hitDirection.collider.gameObject;
                        break;
                    case 2:
                        canMoveRight = true;
                        nodeRight = hitDirection.collider.gameObject;
                        break;
                    case 3:
                        canMoveLeft = true;
                        nodeLeft = hitDirection.collider.gameObject;
                        break;
                }
            }
        }
    }


    // getting the automatic next node using the current direction
    public GameObject setNextNode(string direction)
    {
        if (direction == "up" && canMoveUp)
        {
            theDirection = Vector2.up;
            return nodeUp;
        }
        else if (direction == "down" && canMoveDown)
        {
            theDirection = -Vector2.up;
            return nodeDown;
        }
        else if (direction == "right" && canMoveRight)
        {
            theDirection = Vector2.right;
            return nodeRight;
        }
        else if (direction == "left" && canMoveLeft)
        {
            theDirection = -Vector2.right;
            return nodeLeft;
        }
        else return null;
    }
}
