using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameManager gameManager;


    // current node is already referenced in unity
    public GameObject currentNode;
    private float speed = 12;

    // this three types of string will refer to the direction that will be used to make the movement
    // the four string values will be one of these {up, down, right, left}
    public string nextDirection = "";
    public string direction = "";
    public string tempDirection = "";

    // openGate will be used to manage the 4 Gates which in the right and left 
    public bool openGate = true;


    private void Awake()
    {
        // selecting the GameManager component
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        // geting the current node controller which will controll the direction of movement
        NodeController currentNodeC = currentNode.GetComponent<NodeController>();

        // moving pacman to the next node which is actually going to be at the intersections 
         transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, (speed*Time.deltaTime));


        // this condition will make the pacman chenging the direction faster if the new direction is the opposite one
        // so the pacman will not have to be as the same position as the nextnode to make the change of the direction
        bool oppositeDirection = false;
        if ((direction == "right" && nextDirection == "left")
            || (direction == "left" && nextDirection == "right")
            || (direction == "up" && nextDirection == "down")
            || (direction == "down" && nextDirection == "up"))
        {
            oppositeDirection = true;
        }


        // if pacman arrive the target position it will change the direction or just return null
        if ((transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y) || oppositeDirection)
        {

            // if the pacman is on one of the four gates positions it will make a teleporation to the oppsite side
            if (currentNodeC.upRightGate && openGate) {
                teleport(gameManager.upLeftGateNode, "right");
            }
            else if (currentNodeC.downRightGate && openGate) {
                teleport(gameManager.downLeftGateNode, "right");   
            }
            else if (currentNodeC.upLeftGate && openGate) {
                teleport(gameManager.upRightGateNode, "left");  
            }
            else if (currentNodeC.downLeftGate && openGate) {
                teleport(gameManager.downRightGateNode, "left");  
            }
            else {
                GameObject nextNode;
                // this condition will happen if the player typed the arrow key before it get the intersection but the return was null means
                // there was not any node in that direction so it the direction equalized with the nextDirection
                // but however the tempDirecion is still the same
                if (direction == nextDirection) {
                    nextNode = currentNodeC.setNextNode(tempDirection);

                } // otherwise the nextDirection that was referenced to the nextNode != null which mean the direciton was updated
                else {
                    nextNode = currentNodeC.setNextNode(nextDirection);
                }

                // if the nextnode is not null which mean it's on the target intersection position so it will use the tempDireciton
                // which is guarantee that the the will choose the last direction typed by the arrow keys input
                if (nextNode != null) {
                    currentNode = nextNode;
                    direction = tempDirection;
                    // get the angle of the using arctan
                    float angle = Mathf.Atan2(currentNodeC.theDirection.y, currentNodeC.theDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                } // or it just going to the next node with the same last moving direciton
                else {
                    nextDirection = direction;
                    nextNode = currentNodeC.setNextNode(nextDirection);
                    
                    if (nextNode != null) {
                        currentNode = nextNode;
                    }
                }
            }
        }
        else {
            openGate = true;
        }
    }

    // changing the current position of pacman to the opposite side ... and this function will be used only by the gates
    private void teleport(GameObject node, string new_direction)
    {
        currentNode = node;
        direction = new_direction;
        nextDirection = new_direction;
        tempDirection = new_direction;
        transform.position = currentNode.transform.position;
        openGate = false;
    }


    // set direciton will be used another scripts
    public void SetDirection(string newDireciton)
    {
        nextDirection = newDireciton;
        tempDirection = newDireciton;
    }

}
