using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    // Is referenced to the palletExploration of node in position (-0.5, -8.5)
    public PalletExploration palletExploration;
    public A_Star aStar;
    
    // all pallets in the game are stored in foods list
    List<GameObject> foods;
    
    // pacman node is the start node to start the search from
    public GameObject pacmanNode;
    // target node is the goal node and it will be the nearest one from pacman
    public GameObject targetNode;

    List<GameObject> path;
    // speed of pacman
    private float speed = 30;

    // the position of next node
    public Vector3 nextNode;
    int currentIndex;


    private void Start()
    {
        // using the BFS to store all pallets in foods list
        foods = palletExploration.BFS_Exploration();
        aStar = gameObject.GetComponent<A_Star>();

        ResetThePath();


        //For debuging------------------------------------
        // This code for debuging ... it check that the the list of all node are recieved from 
        // palletExploration script, but make sure that in Player inspector in unity interface that 
        // the AIPlayerController(PalletExplorating) it contain Node(Pallet Exploration)
        //int nodeIndex = 0;
        //foreach (GameObject go in foods)
        //{
        //    Debug.Log("node" + nodeIndex + go.transform.position);
        //    nodeIndex++;
        //}
        //For debuging------------------------------------
    }



    private void Update()
    {
        // the next node to make pacman moving to 
        nextNode = path[currentIndex].transform.position;
        
        //For debuging------------------------------------
        //Debug.Log("next_node " + nextNode);
        //For debuging------------------------------------
        
        // if the pacman still not arrived to the next node means that the pacman is still between the current 
        // node and the next one
        if (transform.position != nextNode){
            transform.position = Vector2.MoveTowards(transform.position, nextNode, (speed * Time.deltaTime));
            // get the angle of the using arctan
            float angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        } // else the pacman is at the top of the next node 
        else{
            // check that this node is the target node
            if (transform.position == targetNode.transform.position)
            {
                // if it is ... reset the new path
                pacmanNode = targetNode;
                ResetThePath();

            }
            // but if it's not the target will get next node in the path
            else currentIndex++;
        }
        
        //if (foods.Count == 0) gameObject.SetActive(false);
    }


    // set the new start node and target and get the path from the start to the goal
    private void ResetThePath()
    {
        targetNode = DequeueWithPriority(transform.position, foods);
        path = aStar.getPath(pacmanNode, targetNode);
        currentIndex = 0;
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