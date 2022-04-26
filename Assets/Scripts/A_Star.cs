using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Star : MonoBehaviour
{

    
    public List<GameObject> getPath(GameObject start, GameObject target)
    {
        NodeController currentNodeC;

        List<GameObject> path = new List<GameObject>();
        List<Node> priorityQueue = new List<Node>();
        priorityQueue.Add(new Node(start, target));

        Node current_Node;
        
        
        while (true)
        {
            current_Node = GetNodeWithPriority(priorityQueue);

            if (current_Node.currentNode == target)
            {
                int steps = current_Node.steps_from_start;
                for (int i = 0; i < steps; i++)
                {
                    path.Add(current_Node.currentNode);
                    current_Node = current_Node.parent;

                }
                break;           
            }
            currentNodeC = current_Node.currentNode.GetComponent<NodeController>();
            
            if (currentNodeC.canMoveUp) priorityQueue.Add(new Node(currentNodeC.nodeUp, target, current_Node));
            if (currentNodeC.canMoveRight) priorityQueue.Add(new Node(currentNodeC.nodeRight, target, current_Node));
            if (currentNodeC.canMoveDown) priorityQueue.Add(new Node(currentNodeC.nodeDown, target, current_Node));
            if (currentNodeC.canMoveLeft) priorityQueue.Add(new Node(currentNodeC.nodeLeft, target, current_Node));
        }
       return path;
    }



    private Node GetNodeWithPriority(List<Node> queue)
    {
        int index = 0;
        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i].cost < queue[index].cost)
            {
                index = i;
            }
        }
        Node selectedNode = queue[index];
        queue.RemoveAt(index);
        return selectedNode;
    }
}




class Node
{
    public GameObject currentNode;
    public GameObject target;
    public Node parent;
    public float cost = 0;
    public int steps_from_start;


    public Node(GameObject current_Node, GameObject goal)
    {
        currentNode = current_Node;
        target = goal;
        parent = null;
        steps_from_start = 0;
        cost = steps_from_start + manhattan(currentNode, target);    
    }


    public Node(GameObject current_Node, GameObject goal, Node p)
    {
        currentNode=current_Node;
        target=goal;
        parent = p;
        steps_from_start = parent.steps_from_start + 1;
        cost = steps_from_start + manhattan(currentNode, target);
    }


    public float manhattan(GameObject currentNode, GameObject target)
    {
        return Mathf.Abs(currentNode.transform.position.x - target.transform.position.x) + Mathf.Abs(currentNode.transform.position.y - target.transform.position.y);
    }
}