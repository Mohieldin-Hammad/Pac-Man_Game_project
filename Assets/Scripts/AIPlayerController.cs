using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    // Is referenced to the palletExploration of node in position (-0.5, -8.5)
    public PalletExploration palletExploration;
    public A_Star aStar;
    
    List<GameObject> foods;
    
    public GameObject pacmanNode;
    public GameObject targetNode;

    List<GameObject> path;
    private float speed = 4;



    private void Awake()
    {
        
    }
    
    
    private void Start()
    {
        transform.position = pacmanNode.transform.position;
        foods = palletExploration.BFS_Exploration();
        aStar = gameObject.GetComponent<A_Star>();
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

        targetNode = DequeueWithPriority(foods);

        ////For debuging------------------------------------
        //Debug.Log("node " + targetNode.transform.position);
        ////For debuging------------------------------------

        path = aStar.getPath(pacmanNode, targetNode);

        ////for debuging------------------------------------
        //for (int i = 0; i < path.Count; i++)
        //{
        //    Debug.Log("node " + path[i]);
        //}
        ////for debuging------------------------------------

        foreach (GameObject node in path)
        {
            transform.position = Vector2.MoveTowards(transform.position, node.transform.position, (speed * Time.deltaTime));
            while (transform.position != targetNode.transform.position)
            {

            }
        }

        pacmanNode = targetNode;

        if (foods.Count == 0) gameObject.SetActive(false);
    }


    public static GameObject DequeueWithPriority(List<GameObject> nodes)
    {
        int index = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].GetComponent<PalletController>().manhattanDistance < nodes[index].GetComponent<PalletController>().manhattanDistance)
            {
                index = i;
            }
        }
        GameObject selectedNode = nodes[index];
        nodes.RemoveAt(index);
        return selectedNode;
    }
}