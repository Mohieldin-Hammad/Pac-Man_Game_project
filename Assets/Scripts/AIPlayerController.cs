using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    public PalletExploration palletExploration;
    public A_Star aStar;
    
    List<GameObject> foods;
    
    public GameObject pacmanNode;
    public GameObject targetNode;

    List<GameObject> path;
    private float speed = 4;



    private void Awake()
    {
        palletExploration = GetComponent<PalletExploration>();
        aStar = GetComponent<A_Star>();
        //foodsQueue = new PriorityQueue(palletExploration.BFS_Exploration());
        palletExploration = new PalletExploration();

    }
    
    
    private void Start()
    {
        transform.position = pacmanNode.transform.position;
        foods = palletExploration.BFS_Exploration();
    }

    

    private void Update()
    {
        //targetNode = foodsQueue.Dequeue();
        targetNode = GetNodeWithPriority(foods);
        //------------------------
        Debug.Log(targetNode);

        path = aStar.getPath(pacmanNode, targetNode);

        foreach (GameObject node in path)
        {
            transform.position = Vector2.MoveTowards(transform.position, node.transform.position, (speed * Time.deltaTime));
            while (transform.position != targetNode.transform.position)
            {
                      
            }
        }
        pacmanNode = targetNode;
        
    }


    public static GameObject GetNodeWithPriority(List<GameObject> food)
    {
        int index = 0;
        for (int i = 0; i < food.Count; i++)
        {
            if (food[i].GetComponent<PalletController>().manhattanDistance < food[index].GetComponent<PalletController>().manhattanDistance)
            {
                index = i;
            }
        }
        GameObject selectedNode = food[index];
        food.RemoveAt(index);
        return selectedNode;
    }
}