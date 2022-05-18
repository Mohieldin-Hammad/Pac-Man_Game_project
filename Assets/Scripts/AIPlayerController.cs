using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    //// Is referenced to the palletExploration of node in position (-0.5, -8.5)
    //public PalletExploration palletExploration;
    public A_Star aStar;
    
    // all pallets in the game are stored in foods list
    List<GameObject> foods;
    public int foodsCount { get; private set; }

    // pacman node is the start node to start the search from
    public GameObject pacmanNode;

    // target node is the goal node and it will be the nearest one from pacman
    public GameObject targetNode;

    List<GameObject> path;
    // speed of pacman
    private float speed = 50;

    // the position of next node
    public Vector3 nextNode { get; private set; }
    
    public int currentIndex { get; private set; }
    
    // refernce form GameManger class will do this refernce in unity interface
    public GameManager gameManager;


    private float angle = 0f;

    private void Start()
    {
        currentIndex = 0;
        aStar = gameObject.GetComponent<A_Star>();
    }



    private void Update()
    {
        if (GameManager.reStartGame)
        {
            ReStart();
            GameManager.reStartGame = false;
        }

        // the next node to make pacman moving to 
        nextNode = path[currentIndex].transform.position;

        
        if (nextNode.x > transform.position.x && nextNode.y == transform.position.y) {
            angle = 0f;
        }
        else if (nextNode.x < transform.position.x && nextNode.y == transform.position.y) {
            angle = 180f;
        }
        else if (nextNode.y > transform.position.y && nextNode.x == transform.position.x) {
            angle = 90f;
        }
        else if (nextNode.y < transform.position.y && nextNode.x == transform.position.x) {
            angle = 270f;
        }
        // changing the rotation of pacman depending on the angle
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        // if the pacman still not arrived to the next node means that the pacman is still between the current 
        // node and the next one
        if (transform.position != nextNode){
            transform.position = Vector2.MoveTowards(transform.position, nextNode, (speed * Time.deltaTime));

        } // else the pacman is at the top of the next node 
        else{
            // check that this node is the target node
            if (transform.position == targetNode.transform.position && gameManager.score < foodsCount)
            {
                // if it is ... reset the new path
                pacmanNode = targetNode;
                ResetThePath();
            }
            // but if it's not the target will get next node in the path
            else currentIndex++;
        }
    }



    // Get the list of all the pallets nodes in the game and transform the pacman to it's default 
    // position... it also reset the path to make the next move
    public void ReStart()
    {
        foods = gameManager.getFoods();
        foodsCount = foods.Count;
        transform.position = gameManager.originalPacmanPosition;
        transform.rotation = gameManager.originalPacmanRotation;
        pacmanNode = gameManager.getOriginalNode();
        ResetThePath();
    }



    // set the new start node and target and get the path from the start to the goal
    private void ResetThePath()
    {
        if (foods.Count > 0) { 
        targetNode = DequeueWithPriority(transform.position, foods);
        path = aStar.getPath(pacmanNode, targetNode);
        currentIndex = 0;
        }
    }



    // the priority here is the distance between the start pacman and the nearest goal
    public static GameObject DequeueWithPriority(Vector3 position, List<GameObject> nodes)
    {
        int index = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (GetManhattanDistance(position, nodes[i]) < GetManhattanDistance(position, nodes[index]))
            {
                index = i;
            }
        }
        GameObject selectedNode = nodes[index];
        nodes.RemoveAt(index);
        return selectedNode;
    }


    // calculate the manhattan distance to be used in DequeueWithPriority method
    public static float GetManhattanDistance(Vector3 position, GameObject node)
    {
        return Mathf.Abs(position.x - node.transform.position.x) + Mathf.Abs(position.y - node.transform.position.y);
    }
}
